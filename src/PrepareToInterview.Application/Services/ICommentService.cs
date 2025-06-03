using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Services
{
    public interface ICommentService
    {
        Task<IDataResult<CommentGetByIdDto>> GetByIdAsync(int id);
        Task<IDataResult<PagedResponse<CommentListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<IDataResult<PagedResponse<CommentListDto>>> GetCommentsByAnswerIdAsync(int answerId, int pageNumber = 1, int pageSize = 10);
        Task<IDataResult<CommentCreatedDto>> CreateAsync(CommentCreateDto commentCreateDto);
        Task<IDataResult<CommentUpdatedDto>> UpdateAsync(CommentUpdateDto commentUpdateDto);
        Task<IResult> DeleteAsync(int id);
    }
} 