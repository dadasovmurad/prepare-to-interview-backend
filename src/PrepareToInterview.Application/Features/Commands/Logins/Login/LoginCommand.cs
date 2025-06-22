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
                var appUser = await _userReadRepository.GetUserByPassKeyAsync(request.PassKey);

                if (appUser is null)
                {
                    return new ErrorDataResult<AppUserDto>(Messages.InvalidPassKey);
                }

                return new SuccessDataResult<AppUserDto>(_mapper.Map<AppUserDto>(appUser));
            }
        }
    }
}