using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Features.Base;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBuddy.Models.Paging;

namespace PrepareToInterview.Application.Features.Queries.Categories.GetAllCategory
{
    public class GetAllCategoriesQuery : IRequest<IDataResult<IList<CategoryDto>>>
    {
        public string Lang { get; set; } = "en";

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
                var categories = await _categoryReadRepository.GetAll()
                                                      .Include(c => c.CategoryTranslations.Where(tran => tran.LanguageCode == request.Lang))
                                                      .ToListAsync();


                var resultData = _mapper.Map<List<CategoryDto>>(categories);

                return new SuccessDataResult<List<CategoryDto>>(resultData);
            }
        }
    }
}
