using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using PrepareToInterview.Application.DTOs.User;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Utilities.Helpers;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest<IResult>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? PersonalUrl { get; set; }
        public string? ImageUrl { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IMapper _mapper;
            public CreateUserCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _mapper = mapper;
            }
            public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var trimmedUsername = request.Username.Trim();
                var trimmedEmail = request.Email.Trim();

                var userExists = await _userReadRepository.GetAsync(
                    x => x.Username == trimmedUsername || x.Email == trimmedEmail
                );

                if (userExists is not null)
                    return new ErrorResult("Username or email already exists.");

                var user = _mapper.Map<AppUser>(request);

                var passKeyResult = PassKeyHelper.GenerateAndHashPassKey();
                user.PassKeyHash = passKeyResult.HashedKey;

                await _userWriteRepository.AddAsync(user);
                await _userWriteRepository.SaveAsync();

                // return the plain key in a DTO
                var resultDto = new UserCreatedDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    PlainPassKey = passKeyResult.PlainKey
                };

                return new SuccessDataResult<UserCreatedDto>(resultDto, "User successfully created.");
            }
        }
    }
}