using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Features.Base;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBuddy.Models.Paging;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion
{
    public class GetAllQuestionsQuery : IRequest<IDataResult<IList<QuestionListDto>>>
    {
        public string Lang { get; set; } = "en";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionsQuery, IDataResult<IList<QuestionListDto>>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;
            public GetAllQuestionQueryHandler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IList<QuestionListDto>>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
            {
                var includedData = await _questionReadRepository.GetAll()
                                                         //.Include(q => q.Category)
                                                         //.Include(q => q.Category.CategoryTranslations.Where(c => c.LanguageCode == request.Lang))
                                                         .Include(q => q.QuestionTranslations.Where(t => t.LanguageCode == request.Lang))
                                                          //.Include(q => q.Answers)
                                                          //.Include(q => q.Comments)
                                                          //.Include(q => q.QuestionTags)  
                                                          //.ThenInclude(x => x.Tag)
                                                          .Skip((request.PageNumber - 1) * request.PageSize) // Skip items from previous pages
                                                          .Take(request.PageSize) // Take items for the current page
                                                          .ToListAsync(); // Convert to a list asynchronously

                var resultData = _mapper.Map<IList<QuestionListDto>>(includedData);

                return new SuccessDataResult<IList<QuestionListDto>>(resultData);
            }
        }
    }
}