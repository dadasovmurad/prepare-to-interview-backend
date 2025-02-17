using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Features.Queries.Categories.GetAllCategory;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Categories.GetCategoryChildrensById
{
    public class GetCategoryChildrensById : IRequest<IDataResult<IList<CategoryDto>>>
    {
        public int Id { get; set; }
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetCategoryChildrensById, IDataResult<IList<CategoryDto>>>
        {
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly IMapper _mapper;

            public GetAllCategoriesQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper)
            {
                _categoryReadRepository = categoryReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IList<CategoryDto>>> Handle(GetCategoryChildrensById request, CancellationToken cancellationToken)
            {
                var categories = await _categoryReadRepository.GetAll(tracking: false)
                                                     //.Where(c => c.ParentId == request.Id)
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

                var resultData = categories.Where(c => c.ParentId == request.Id)
                                           .ToList();

                return new SuccessDataResult<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(resultData));
                //return new SuccessDataResult<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(categories));
            }
        }
    }
}
