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

namespace PrepareToInterview.Application.Features.Queries.Question.GetAllQuestion
{
    public class GetAllQuestionQuery : IRequest<IDataResult<QuestionListModel>>
    {
        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, IDataResult<QuestionListModel>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;

            public GetAllQuestionQueryHandler(IQuestionReadRepository questionReadRepository)
            {
                _questionReadRepository = questionReadRepository;
            }

            public async Task<IDataResult<QuestionListModel>> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
            {
                var includedData = await _questionReadRepository.GetAll()
                                                         .Include(q => q.Answer)
                                                         .Include(q => q.Comments)
                                                         .Select(x => new QuestionListDto
                                                         {
                                                             Id = x.Id,
                                                             Content = x.Content,
                                                             Category = x.Category,
                                                             SuitableFor = x.SuitableFor,
                                                             Answers = x.Answer.Select(a => new { a.Id, a.Content }).ToList(),
                                                             Comments = x.Comments.Select(c => new { c.Id, c.Content }).ToList()
                                                         }).ToListAsync();

                QuestionListModel questionListModel = new QuestionListModel()
                {
                    Items = includedData
                };

                return new SuccessDataResult<QuestionListModel>(questionListModel);
            }
        }
    }
}
