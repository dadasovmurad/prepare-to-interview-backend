using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetRelatedQuestions
{
    public class GetRelatedQuestionsQuery : IRequest<IDataResult<List<QuestionRelatedDto>>>
    {
        public int QuestionId { get; set; }

        public class GetRelatedQuestionsHandler : IRequestHandler<GetRelatedQuestionsQuery, IDataResult<List<QuestionRelatedDto>>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;
            public GetRelatedQuestionsHandler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<List<QuestionRelatedDto>>> Handle(GetRelatedQuestionsQuery request, CancellationToken cancellationToken)
            {
                var question = await _questionReadRepository.GetAll(x => x.Id == request.QuestionId)
                                                            .Include(u => u.User)
                                                            .Include(q => q.QuestionTags)
                                                            .ThenInclude(q => q.Tag)
                                                            .FirstOrDefaultAsync();
                if (question is null)
                    return new ErrorDataResult<List<QuestionRelatedDto>>("Question not found!");

                var questionTagIds = question.QuestionTags.Select(qt => qt.TagID).ToList();
                int questionCategoryId = question.CategoryId;
                var relatedQuestions = await _questionReadRepository.GetAll()
                                              .Include(u => u.User)
                                              .Where(q => q.Id != request.QuestionId &&
                                                         (q.CategoryId == questionCategoryId ||
                                                          q.QuestionTags.Any(qt => questionTagIds.Contains(qt.TagID))))
                                              .OrderBy(q => Guid.NewGuid())
                                              .Take(10)
                                              .ToListAsync();

                var resultData = _mapper.Map<List<QuestionRelatedDto>>(relatedQuestions);

                return new SuccessDataResult<List<QuestionRelatedDto>>(resultData);
            }
        }
    }
}