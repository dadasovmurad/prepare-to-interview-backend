using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Features.Base;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
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
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly IMapper _mapper;
            public GetAllQuestionQueryHandler(IMapper mapper, IQuestionReadRepository questionReadRepository, ICategoryReadRepository categoryReadRepository)
            {
                _mapper = mapper;
                _questionReadRepository = questionReadRepository;
                _categoryReadRepository = categoryReadRepository;
            }

            public async Task<IDataResult<PagedResponse<QuestionListDto>>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
            {
                var questions = _questionReadRepository.GetAll()
                                                                        .Include(q => q.Category)
                                                                        .Include(t => t.QuestionTags)
                                                                        .ThenInclude(t => t.Tag)
                                                                        .AsQueryable();


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
                if (!string.IsNullOrWhiteSpace(request.Category) || !string.IsNullOrWhiteSpace(request.SubCategory))
                {
                    var allCategories = await _categoryReadRepository.GetAll(tracking: false).ToListAsync();
                    var rootCategories = BuildCategoryTree(allCategories);

                    var matchedCategoryIds = new List<int>();

                    if (!string.IsNullOrWhiteSpace(request.Category) && string.IsNullOrWhiteSpace(request.SubCategory))
                    {
                        // Only Category provided
                        var categoryNode = FindCategoryByName(rootCategories, request.Category);
                        if (categoryNode != null)
                            CollectAllCategoryIds(categoryNode, matchedCategoryIds);
                    }
                    else if (!string.IsNullOrWhiteSpace(request.SubCategory) && string.IsNullOrWhiteSpace(request.Category))
                    {
                        // Only SubCategory provided
                        var subCategoryNode = FindCategoryByName(rootCategories, request.SubCategory);
                        if (subCategoryNode != null)
                            matchedCategoryIds.Add(subCategoryNode.Id);
                    }
                    else if (!string.IsNullOrWhiteSpace(request.Category) && !string.IsNullOrWhiteSpace(request.SubCategory))
                    {
                        // Both provided: find subcategory under specified category
                        var categoryNode = FindCategoryByName(rootCategories, request.Category);
                        if (categoryNode?.Children != null)
                        {
                            var subCategoryNode = categoryNode.Children
                                .FirstOrDefault(c => c.Name.Equals(request.SubCategory, StringComparison.OrdinalIgnoreCase));

                            if (subCategoryNode != null)
                                matchedCategoryIds.Add(subCategoryNode.Id);
                        }
                    }
                    if (matchedCategoryIds.Any())
                    {
                        questions = questions.Where(q => matchedCategoryIds.Contains(q.CategoryId));
                    }
                }


                var pagedResult = await questions.GetPageAsync(request.PageNumber, request.PageSize);

                var resultData = _mapper.Map<PagedResponse<QuestionListDto>>(pagedResult);

                return new SuccessDataResult<PagedResponse<QuestionListDto>>(resultData);
            }
            private Category? FindCategoryByName(List<Category> nodes, string name)
            {
                foreach (var node in nodes)
                {
                    if (node.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return node;

                    if (node.Children != null)
                    {
                        var found = FindCategoryByName(node.Children.ToList(), name);
                        if (found != null)
                            return found;
                    }
                }
                return null;
            }
            private void CollectAllCategoryIds(Category category, List<int> ids)
            {
                ids.Add(category.Id);
                if (category.Children != null)
                {
                    foreach (var child in category.Children)
                    {
                        CollectAllCategoryIds(child, ids);
                    }
                }
            }
            private List<Category> BuildCategoryTree(List<Category> allCategories)
            {
                var lookup = allCategories.ToDictionary(c => c.Id);
                foreach (var category in allCategories)
                {
                    if (category.ParentId.HasValue && lookup.TryGetValue(category.ParentId.Value, out var parent))
                    {
                        parent.Children ??= new List<Category>();
                        parent.Children.Add(category);
                    }
                }
                return allCategories.Where(c => !c.ParentId.HasValue).ToList(); // root categories
            }
        }
    }
}