using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Services
{
    public interface IAnswerService
    {
        Task<IDataResult<AnswerGetByIdDto>> GetByIdAsync(int id);
        Task<IDataResult<PagedResponse<AnswerListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<IDataResult<PagedResponse<AnswerListDto>>> GetAnswersByQuestionIdAsync(int questionId, int pageNumber = 1, int pageSize = 10);
        Task<IDataResult<AnswerCreatedDto>> CreateAsync(AnswerCreateDto answerCreateDto);
        Task<IDataResult<AnswerUpdatedDto>> UpdateAsync(AnswerUpdateDto answerUpdateDto);
        Task<IResult> DeleteAsync(int id);
    }
}