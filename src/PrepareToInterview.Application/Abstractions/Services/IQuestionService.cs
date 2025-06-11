using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Abstractions.Services
{
    public interface IQuestionService
    {
        Task<Result> CreateAsync();
        Task<DataResult<QuestionDto>> GetAllAsync();
        Task<Dat>
    }
}
