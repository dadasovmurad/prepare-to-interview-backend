using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetTotalQuestionCount
{
    public class GetTotalQuestionCountQuery : IRequest<IDataResult<int>>
    {

        public class GetTotalQuestionCountHandler : IRequestHandler<GetTotalQuestionCountQuery, IDataResult<int>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;

            public GetTotalQuestionCountHandler(IQuestionReadRepository questionReadRepository)
            {
                _questionReadRepository = questionReadRepository;
            }

            public async Task<IDataResult<int>> Handle(GetTotalQuestionCountQuery request, CancellationToken cancellationToken)
            {
                int count = await _questionReadRepository.Table.CountAsync();
                return new SuccessDataResult<int>(count);
            }
        }
    }
}
