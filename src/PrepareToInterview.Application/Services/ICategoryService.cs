using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Services
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryGetByIdDto>> GetByIdAsync(int id);
        Task<IDataResult<PagedResponse<CategoryListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<IDataResult<CategoryCreatedDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<IDataResult<CategoryUpdatedDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IResult> DeleteAsync(int id);
    }
} 