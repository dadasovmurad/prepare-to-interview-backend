using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Question.GetByIdQuestion
{
    public class GetByIdQuestionQuery : IRequest<IDataResult<QuestionGetByIdDto>>
    {
        public string Id { get; set; }

        public class GetBuIdQuestionQueryHanler : IRequestHandler<GetByIdQuestionQuery, IDataResult<QuestionGetByIdDto>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;

            public GetBuIdQuestionQueryHanler(IQuestionReadRepository questionReadRepository)
            {
                _questionReadRepository = questionReadRepository;
            }

            public async Task<IDataResult<QuestionGetByIdDto>> Handle(GetByIdQuestionQuery request, CancellationToken cancellationToken)
            {
                var targetQuestion = await _questionReadRepository.GetAll(q => q.Id == Guid.Parse(request.Id))
                                                        .Include(q => q.Answer)
                                                        .Include(q => q.Comments)
                                                        .FirstAsync();
                if (targetQuestion is not null)
                {
                    var questionGetById = new QuestionGetByIdDto
                    {
                        Id = targetQuestion.Id,
                        Content = targetQuestion.Content,
                        Category = targetQuestion.Category,
                        SuitableFor = targetQuestion.SuitableFor,
                        Answers = targetQuestion.Answer.Select(a => new { a.Id, a.Content }),
                        Comments = targetQuestion.Comments.Select(c => new { c.Id, c.Content }),
                    };
                    return new SuccessDataResult<QuestionGetByIdDto>(questionGetById);
                }
                return new ErrorDataResult<QuestionGetByIdDto>();
            }
        }
    }
}
