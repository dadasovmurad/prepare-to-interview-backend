using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Features.Base;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion
{
    public class GetAllQuestionsQuery : BasePagedQuery<IDataResult<PagedResponse<QuestionListDto>>>
    {
        //public string Lang { get; set; } = "en";
        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionsQuery, IDataResult<PagedResponse<QuestionListDto>>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;
            public GetAllQuestionQueryHandler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<PagedResponse<QuestionListDto>>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
            {
                
                var includedData = await _questionReadRepository.GetAll()
                    //.Where(x=>x.Id==3139)
                                                         //.Include(q => q.Category)
                                                         //.Include(q => q.Category.CategoryTranslations.Where(c => c.LanguageCode == request.Lang))
                                                         //.Include(q => q.QuestionTranslations.Where(t => t.LanguageCode == request.Lang))
                                                          //.Include(q => q.Answers)
                                                          //.Include(q => q.Comments)
                                                          //.Include(q => q.QuestionTags)  
                                                          //.ThenInclude(x => x.Tag)
                                                          //.Skip((request.PageNumber - 1) * request.PageSize) // Skip items from previous pages
                                                          //.Take(request.PageSize) // Take items for the current page
                                                          .GetPageAsync(request.PageNumber, request.PageSize);

                var resultData = _mapper.Map<PagedResponse<QuestionListDto>>(includedData);

                return new SuccessDataResult<PagedResponse<QuestionListDto>>(resultData);
            }
        }
    }
}