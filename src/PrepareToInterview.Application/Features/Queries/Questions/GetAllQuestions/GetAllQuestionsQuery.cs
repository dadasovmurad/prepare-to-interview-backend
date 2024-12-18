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
    public class GetAllQuestionsQuery : BasePagedQuery<PagedResponse<QuestionListDto>>
    {
        public string Lang { get; set; } = "en";
        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionsQuery, PagedResponse<QuestionListDto>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;
            public GetAllQuestionQueryHandler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<QuestionListDto>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
            {
                var includedData = await _questionReadRepository.GetAll()
                                                         //.Include(q => q.Category)
                                                         //.Include(q => q.Category.CategoryTranslations.Where(c => c.LanguageCode == request.Lang))
                                                         .Include(q => q.QuestionTranslations.Where(t => t.LanguageCode == request.Lang))
                                                         //.Include(q => q.Answers)
                                                         //.Include(q => q.Comments)
                                                         //.Include(q => q.QuestionTags)  
                                                         //.ThenInclude(x => x.Tag)
                                                         .GetPageAsync(request.PageNumber, request.PageSize);

                var dto = _mapper.Map<PagedResponse<QuestionListDto>>(includedData);
                return _mapper.Map<PagedResponse<QuestionListDto>>(includedData);
            }
        }
    }
}