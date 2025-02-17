using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Features.Queries.Categories.GetAllCategory;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Categories.GetCategoryHeaders
{
    public class GetCategoryHeadersQuery : IRequest<IDataResult<IList<CategoryHeaderDto>>>
    {
        public class GetCategoryHeadersQueryHandler : IRequestHandler<GetCategoryHeadersQuery, IDataResult<IList<CategoryHeaderDto>>>
        {
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly IMapper _mapper;
            public GetCategoryHeadersQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper)
            {
                _categoryReadRepository = categoryReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IList<CategoryHeaderDto>>> Handle(GetCategoryHeadersQuery request, CancellationToken cancellationToken)
            {
                var categories = await _categoryReadRepository.GetAll(tracking: false)
                                                              .Where(category => category.Parent == null)
                                                              .ToListAsync(cancellationToken);

                return new SuccessDataResult<List<CategoryHeaderDto>>(_mapper.Map<List<CategoryHeaderDto>>(categories));
            }
        }
    }
}
