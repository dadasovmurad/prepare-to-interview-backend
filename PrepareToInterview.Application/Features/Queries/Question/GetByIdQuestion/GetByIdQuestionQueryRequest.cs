using MediatR;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Question.GetByIdQuestion
{
    public class GetByIdQuestionQueryRequest:IRequest<IDataResult<GetByIdQuestionQueryResponse>>
    {
        public string Id { get; set; }
    }
}
