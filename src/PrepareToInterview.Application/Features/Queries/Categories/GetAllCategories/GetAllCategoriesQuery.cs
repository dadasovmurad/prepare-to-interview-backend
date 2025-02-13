using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Queries.Categories.GetAllCategory
{
    public class GetAllCategoriesQuery : IRequest<IDataResult<IList<CategoryDto>>>
    {
        //public string Lang { get; set; } = "en";

        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllCategoriesQuery, IDataResult<IList<CategoryDto>>>
        {
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly IMapper _mapper;

            public GetAllQuestionQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper)
            {
                _categoryReadRepository = categoryReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IList<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = await _categoryReadRepository.GetAll(tracking: false)
                                                      //.Include(c => c.CategoryTranslations.Where(tran => tran.LanguageCode == request.Lang))
                                                      //.Where(c => c.Parent == null)
                                                      //.Include(c => c.Children)
                                                      .ToListAsync();

                var categoryDict = categories.ToDictionary(c => c.Id);

                foreach (var category in categories)
                {
                    // Find the parent category
                    if (category.ParentId.HasValue && categoryDict.TryGetValue(category.ParentId.Value, out var parentCategory))
                    {
                        parentCategory.Children ??= new List<Category>();
                        // Add the current category to the parent's children
                        parentCategory.Children.Add(category);
                    }
                }

                var resultData = categories.Where(c => !c.ParentId.HasValue);

                return new SuccessDataResult<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(resultData));
            }
        }
    }
}
