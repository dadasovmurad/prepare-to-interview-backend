using AutoMapper;
using MediatR;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Categories.CreateCategory
{
    public class CreateCategoryCommand : IRequest<IDataResult<CategoryCreatedDto>>
    {
        public int? ParentId { get; set; }
        public IList<CategoryTranslationsListDto> CategoryTranslations { get; set; }
        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IDataResult<CategoryCreatedDto>>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryWriteRepository _categoryWriteRepository;

            public CreateCategoryCommandHandler(IMapper mapper, ICategoryWriteRepository categoryWriteRepository)
            {
                _mapper = mapper;
                _categoryWriteRepository = categoryWriteRepository;
            }

            public async Task<IDataResult<CategoryCreatedDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<Category>(request);
                await _categoryWriteRepository.AddAsync(category);
                await _categoryWriteRepository.SaveAsync();
                return new SuccessDataResult<CategoryCreatedDto>();
            }
        }
    }
}