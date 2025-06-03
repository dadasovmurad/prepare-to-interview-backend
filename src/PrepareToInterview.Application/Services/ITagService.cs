using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Services
{
    public interface ITagService
    {
        Task<IDataResult<TagGetByIdDto>> GetByIdAsync(int id);
        Task<IDataResult<PagedResponse<TagListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<IDataResult<TagCreatedDto>> CreateAsync(TagCreateDto tagCreateDto);
        Task<IDataResult<TagUpdatedDto>> UpdateAsync(TagUpdateDto tagUpdateDto);
        Task<IResult> DeleteAsync(int id);
    }
} 