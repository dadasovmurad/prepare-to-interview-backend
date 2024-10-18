using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.Abstractions.Services;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.DTOs.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Persistence.Services
{
    public class QuestionService : IQuestionService
    {
        IQuestionReadRepository _questionReadRepository;
        ICommentReadRepository _commentReadRepository;
        IAnswerReadRepository _answerReadRepository;

        public QuestionService(IQuestionReadRepository questionReadRepository, ICommentReadRepository commentReadRepository, IAnswerReadRepository answerReadRepository)
        {
            _questionReadRepository = questionReadRepository;
            _commentReadRepository = commentReadRepository;
            _answerReadRepository = answerReadRepository;
        }

        public async Task<List<QuestionListDto>> GetAllAsync()
        {
            var includedData = await _questionReadRepository.GetAll()
                                                        .Include(q => q.Answer)               
                                                        .Include(q => q.Comments)             
                                                        .Select(x => new QuestionListDto(
                                                            x.Id,
                                                            x.Content,
                                                            x.Category,
                                                            x.Answer.Select(a => a.Content).ToList(),  
                                                            x.Comments.Select(c => c.Content).ToList() 
                                                        )).ToListAsync();

            return includedData;
        }
    }
}
