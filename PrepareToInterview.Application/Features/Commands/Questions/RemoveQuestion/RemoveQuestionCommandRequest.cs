using MediatR;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Question.RemoveQuestion
{
    public class RemoveQuestionCommandRequest:IRequest<IDataResult<RemoveQuestionCommandResponse>>
    {
        public string Id { get; set; }
    }
}
