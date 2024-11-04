using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetByIdQuestion
{
    public class GetByIdQuestionQuery : IRequest<IDataResult<QuestionGetByIdDto>>
    {
        public string Id { get; set; }

        public class GetBuIdQuestionQueryHanler : IRequestHandler<GetByIdQuestionQuery, IDataResult<QuestionGetByIdDto>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;

            public GetBuIdQuestionQueryHanler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<QuestionGetByIdDto>> Handle(GetByIdQuestionQuery request, CancellationToken cancellationToken)
            {
                var targetQuestion = await _questionReadRepository.GetAll(q => q.Id == Guid.Parse(request.Id))
                                                        .Include(q => q.Answers)
                                                        .Include(q => q.Comments)
                                                        .FirstAsync();
                if (targetQuestion is not null)
                {
                    var resultData = _mapper.Map<QuestionGetByIdDto>(targetQuestion);
                    //var questionGetById = new QuestionGetByIdDto
                    //{
                    //    Id = targetQuestion.Id,
                    //    Content = targetQuestion.Content,
                    //    Category = targetQuestion.Category,
                    //    SuitableFor = targetQuestion.SuitableFor,
                    //    Answers = targetQuestion.Answer.Select(a => new { a.Id, a.Content }),
                    //    Comments = targetQuestion.Comments.Select(c => new { c.Id, c.Content }),
                    //};
                    return new SuccessDataResult<QuestionGetByIdDto>(resultData);
                }
                return new ErrorDataResult<QuestionGetByIdDto>();
            }
        }
    }
}
