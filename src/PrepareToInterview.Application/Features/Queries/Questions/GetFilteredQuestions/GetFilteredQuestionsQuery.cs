using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Features.Base;
using PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Questions.FilterQuestions
{
    public class GetFilteredQuestionsQuery : BasePagedQuery<IDataResult<PagedResponse<QuestionListDto>>>
    {
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? Difficulty { get; set; }
        public string[]? Tags { get; set; }
        public string? Term { get; set; }

        public class GetFilteredQuestionsHandler : IRequestHandler<GetFilteredQuestionsQuery, IDataResult<PagedResponse<QuestionListDto>>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;

            public GetFilteredQuestionsHandler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<PagedResponse<QuestionListDto>>> Handle(GetFilteredQuestionsQuery request, CancellationToken cancellationToken)
            {
                var includedData = _questionReadRepository.GetAll()
                                                          .Include(q => q.Category)
                                                          .Include(t => t.QuestionTags)
                                                          .ThenInclude(t => t.Tag)
                                                          .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Category))
                {
                    includedData = includedData.Where(x => x.Category.Parent == null && x.Category.Name == request.Category);
                }
                if (!string.IsNullOrWhiteSpace(request.SubCategory))
                {
                    includedData = includedData.Where(x => x.Category.Parent != null && x.Category.Name == request.SubCategory);
                }
                if (!string.IsNullOrEmpty(request.Difficulty) && Enum.TryParse<Difficulty>(request.Difficulty, true, out var difficultyEnum))
                {
                    includedData = includedData.Where(x => x.Difficulty == difficultyEnum);
                }
                if (request.Tags is not null && request.Tags.Any())
                {
                    includedData = includedData.Where(x => x.QuestionTags.Any(q => request.Tags.Contains(q.Tag.Name)));
                }
                if (!string.IsNullOrWhiteSpace(request.Term))
                {
                    includedData = includedData.Where(x => x.Content.ToLower().Contains(request.Term.ToLower()));
                }
                var pagedResult = await includedData.GetPageAsync(request.PageNumber, request.PageSize);

                var resultData = _mapper.Map<PagedResponse<QuestionListDto>>(pagedResult);

                return new SuccessDataResult<PagedResponse<QuestionListDto>>(resultData);
            }
        }
    }
}