using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Services
{
    public interface IQuestionService
    {
        Task<IDataResult<QuestionGetByIdDto>> GetByIdAsync(int id);
        Task<IDataResult<PagedResponse<QuestionListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<IDataResult<PagedResponse<QuestionListDto>>> GetFilteredQuestionsAsync(string? category = null, string? subCategory = null, string? difficulty = null, string[]? tags = null, string? term = null, int pageNumber = 1, int pageSize = 10);
        Task<IDataResult<List<QuestionRelatedDto>>> GetRelatedQuestionsAsync(int questionId);
        Task<IDataResult<int>> GetTotalCountAsync();
        Task<IDataResult<QuestionCreatedDto>> CreateAsync(QuestionCreateDto questionCreateDto);
        Task<IDataResult<QuestionUpdatedDto>> UpdateAsync(QuestionUpdateDto questionUpdateDto);
        Task<IResult> DeleteAsync(int id);
    }
} 