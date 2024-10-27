using PrepareToInterview.Application.DTOs.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Abstractions.Services
{
    public interface IQuestionService
    {
        public Task<List<QuestionListDto>> GetAllAsync();
        public Task<QuestionGetByIdDto> GetByIdAsync(string id);
        public Task UpdateAsync(QuestionUpdateDto questionUpdateDto);
        public Task AddAsync(QuestionCreateDto questionCreateDto);
        public Task RemoveAsync(string id);
    }
}
