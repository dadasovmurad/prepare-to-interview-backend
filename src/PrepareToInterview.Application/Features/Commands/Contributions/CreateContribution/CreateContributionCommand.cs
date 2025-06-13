using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Features.Commands.Contributions.CreateContribution
{
    public class CreateContributionCommand : IRequest<IResult>
    {
        public class CreateContributionCommandHandler : IRequestHandler<CreateContributionCommand, IResult>
        {
            public Task<IResult> Handle(CreateContributionCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}