using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Services;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryGetByIdDto>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetAll(c => c.Id == id)
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .FirstOrDefaultAsync();

            if (category is null)
                return new ErrorDataResult<CategoryGetByIdDto>("Category not found");

            var resultData = _mapper.Map<CategoryGetByIdDto>(category);
            return new SuccessDataResult<CategoryGetByIdDto>(resultData);
        }

        public async Task<IDataResult<PagedResponse<CategoryListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var categories = _categoryRepository.GetAll()
                .Include(c => c.Parent)
                .Include(c => c.Children);

            var pagedResult = await categories.GetPageAsync(pageNumber, pageSize);
            var resultData = _mapper.Map<PagedResponse<CategoryListDto>>(pagedResult);
            return new SuccessDataResult<PagedResponse<CategoryListDto>>(resultData);
        }

        public async Task<IDataResult<CategoryCreatedDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();

            return new SuccessDataResult<CategoryCreatedDto>(new CategoryCreatedDto(), "Category successfully created!");
        }

        public async Task<IDataResult<CategoryUpdatedDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoryRepository.GetAll(c => c.Id == categoryUpdateDto.Id)
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .FirstOrDefaultAsync();

            if (category is null)
                return new ErrorDataResult<CategoryUpdatedDto>("Category not found");

            _mapper.Map(categoryUpdateDto, category);
            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();

            return new SuccessDataResult<CategoryUpdatedDto>(new CategoryUpdatedDto(), "Category successfully updated!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return new ErrorResult("Category not found");

            await _categoryRepository.RemoveAsync(id);
            await _categoryRepository.SaveAsync();

            return new SuccessResult("Category successfully deleted!");
        }
    }
} 