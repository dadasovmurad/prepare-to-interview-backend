using PrepareToInterview.Domain.DTOs.Question;
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
    }
}
