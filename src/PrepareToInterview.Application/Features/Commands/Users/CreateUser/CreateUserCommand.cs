using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PrepareToInterview.Application.Constants;
using PrepareToInterview.Application.DTOs.User;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Utilities.Helpers;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest<IDataResult<UserCreatedDto>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? PersonalUrl { get; set; }
        public IFormFile? ImageFile { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IDataResult<UserCreatedDto>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IMapper _mapper;
            private readonly IConfiguration _configuration;
            /// <summary>
            /// Handler for creating a user. Now also sends a passkey email using SMTP settings from configuration.
            /// </summary>
            public CreateUserCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper, IConfiguration configuration)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _mapper = mapper;
                _configuration = configuration;
            }
            public async Task<IDataResult<UserCreatedDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var trimmedUsername = request.Username.Trim();
                var trimmedEmail = request.Email.Trim();

                var userExists = await _userReadRepository.GetAsync(
                    x => x.Username == trimmedUsername || x.Email == trimmedEmail
                );

                if (userExists is not null)
                    return new ErrorDataResult<UserCreatedDto>(Messages.UsernameOrEmailExists);

                var user = _mapper.Map<AppUser>(request);

                // Handle image upload
                if (request.ImageFile != null)
                {
                    try
                    {
                        user.ImageUrl = await FileUploadHelper.UploadImageAsync(request.ImageFile);
                    }
                    catch (ArgumentException ex)
                    {
                        return new ErrorDataResult<UserCreatedDto>(ex.Message);
                    }
                    catch (Exception)
                    {
                        return new ErrorDataResult<UserCreatedDto>("Failed to upload image. Please try again.");
                    }
                }

                var passKeyResult = PassKeyHelper.GenerateAndHashPassKey();
                user.PassKeyHash = passKeyResult.HashedKey;

                await _userWriteRepository.AddAsync(user);
                await _userWriteRepository.SaveAsync();

                var resultDto = _mapper.Map<UserCreatedDto>(user);
                resultDto.PlainPassKey = passKeyResult.PlainKey;

                // Send passkey email
                var smtpSection = _configuration.GetSection("Smtp");
                var smtpHost = smtpSection["Host"];
                var smtpPort = int.Parse(smtpSection["Port"]);
                var smtpUser = smtpSection["User"];
                var smtpPass = smtpSection["Pass"];
                var subject = "Hesabınız üçün giriş açarı";
                var body = $"Hörmətli {user.FullName},<br/>Sizin giriş açarınız: <b>{passKeyResult.PlainKey}</b><br><br>Bu giriş açarı vasitəsilə qeydiyyat etmədən sürətli şəkildə töhfə edə bilərsiniz.";
                await MailHelper.SendEmailAsync(smtpHost, smtpPort, smtpUser, smtpPass, user.Email, subject, body);

                return new SuccessDataResult<UserCreatedDto>(resultDto, Messages.UserSuccessfullyCreated);
            }
        }
    }
}