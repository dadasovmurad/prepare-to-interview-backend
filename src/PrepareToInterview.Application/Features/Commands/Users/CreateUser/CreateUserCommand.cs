using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Features.Commands.Categories.CreateCategory;
using PrepareToInterview.Application.Results;

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
            public Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}