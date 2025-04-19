using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Features.Base;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Enums;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion
{
    public class GetAllQuestionsQuery : BasePagedQuery<IDataResult<PagedResponse<QuestionListDto>>>
    {
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? Difficulty { get; set; }
        public string[]? Tags { get; set; }
        public string? Term { get; set; }

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
                var questions = _questionReadRepository.GetAll()
                                                                        .Include(q => q.Category)
                                                                        .Include(t => t.QuestionTags)
                                                                        .ThenInclude(t => t.Tag)
                                                                        .AsQueryable();
                
                if (!string.IsNullOrWhiteSpace(request.Category))
                {
                    questions = questions.Where(x => x.Category.Parent == null && x.Category.Name == request.Category);
                }
                if (!string.IsNullOrWhiteSpace(request.SubCategory))
                {
                    questions = questions.Where(x => x.Category.Parent != null && x.Category.Name == request.SubCategory);
                }
                if (!string.IsNullOrEmpty(request.Difficulty) && EnumHelper.TryParseEnumOrDescription(request.Difficulty, out Difficulty difficultyEnum))
                {
                    questions = questions.Where(x => x.Difficulty == difficultyEnum);
                }
                if (request.Tags is not null && request.Tags.Any())
                {
                    questions = questions.Where(x => x.QuestionTags.Any(q => request.Tags.Contains(q.Tag.Name)));
                }
                if (!string.IsNullOrWhiteSpace(request.Term))
                {
                    questions = questions.Where(x => x.Content.ToLower().Contains(request.Term.ToLower()));
                }

                var pagedResult = await questions.GetPageAsync(request.PageNumber, request.PageSize);

                var resultData = _mapper.Map<PagedResponse<QuestionListDto>>(pagedResult);

                return new SuccessDataResult<PagedResponse<QuestionListDto>>(resultData);
            }
        }
    }
}