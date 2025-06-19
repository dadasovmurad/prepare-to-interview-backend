using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PrepareToInterview.Application.Constants;
using PrepareToInterview.Application.DTOs.User;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Utilities.Helpers;

namespace PrepareToInterview.Application.Features.Commands.Loginss.Login
{
    public class LoginCommand : IRequest<IDataResult<AppUserDto>>
    {
        public string PassKey { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, IDataResult<AppUserDto>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;
            public LoginCommandHandler(IUserReadRepository userReadRepository, IMapper mapper)
            {
                _userReadRepository = userReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<AppUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var appUsers = await _userReadRepository.GetAll()
                                                        .ToListAsync();

                foreach (var user in appUsers)
                {
                    if (HashingHelper.VerifyHash(request.PassKey, user.PassKeyHash))
                    {
                        return new SuccessDataResult<AppUserDto>(_mapper.Map<AppUserDto>(user));
                    }
                }
                return new ErrorDataResult<AppUserDto>(Messages.InvalidPassKey);
            }
        }
    }
}