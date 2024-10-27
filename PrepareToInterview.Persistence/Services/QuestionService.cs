using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.Abstractions.Services;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
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
        IQuestionWriteRepository _questionWriteRepository;
        ICommentReadRepository _commentReadRepository;
        IAnswerReadRepository _answerReadRepository;

        public QuestionService(IQuestionReadRepository questionReadRepository, IQuestionWriteRepository questionWriteRepository, ICommentReadRepository commentReadRepository, IAnswerReadRepository answerReadRepository)
        {
            _questionReadRepository = questionReadRepository;
            _commentReadRepository = commentReadRepository;
            _answerReadRepository = answerReadRepository;
            _questionWriteRepository = questionWriteRepository;
        }

        public async Task AddAsync(QuestionCreateDto questionCreateDto)
        {
            await _questionWriteRepository.AddAsync(new Question
            {
                Content = questionCreateDto.Content,
                Category = questionCreateDto.Category,
                SuitableFor = questionCreateDto.SuitableFor,
                Answer = questionCreateDto.Answers.Select(x => new Answer { Content = x.Content }).ToList(),
                Comments = questionCreateDto.Comments.Select(x => new Comment { Content = x.Content }).ToList(),
            });
            await _questionWriteRepository.SaveAsync();
        }

        public async Task<List<QuestionListDto>> GetAllAsync()
        {
            var includedData = await _questionReadRepository.GetAll()
                                                        .Include(q => q.Answer)
                                                        .Include(q => q.Comments)
                                                        .Select(x => new QuestionListDto
                                                        {
                                                            Id = x.Id,
                                                            Content = x.Content,
                                                            Category = x.Category,
                                                            SuitableFor = x.SuitableFor,
                                                            Answers = x.Answer.Select(a => new { a.Id, a.Content }).ToList(),
                                                            Comments = x.Comments.Select(c => new { c.Id, c.Content }).ToList()
                                                        }).ToListAsync();

            return includedData;
        }

        public async Task<QuestionGetByIdDto> GetByIdAsync(string id)
        {
            var targetQuestion = await _questionReadRepository.GetAll(q => q.Id == Guid.Parse(id))
                                                        .Include(q => q.Answer)
                                                        .Include(q => q.Comments)
                                                        .FirstAsync();
            if (targetQuestion != null)
            {
                return new QuestionGetByIdDto
                {
                    Id = targetQuestion.Id,
                    Content = targetQuestion.Content,
                    Category = targetQuestion.Category,
                    SuitableFor = targetQuestion.SuitableFor,
                    Answers = targetQuestion.Answer.Select(a => new {a.Id,a.Content}),
                    Comments = targetQuestion.Comments.Select(c => new { c.Id, c.Content }),
                };
            }
            return default;
        }

        public async Task UpdateAsync(QuestionUpdateDto questionUpdateDto)
        {
            var targetQuestion = await _questionReadRepository.GetAll(q => q.Id == Guid.Parse(questionUpdateDto.Id))
                                                              .Include(q => q.Answer)
                                                              .Include(q => q.Comments)
                                                              .FirstOrDefaultAsync();

            if (targetQuestion is not null)
            {
                targetQuestion.Content = questionUpdateDto.Content;
                targetQuestion.SuitableFor = questionUpdateDto.SuitableFor;
                targetQuestion.Category = questionUpdateDto.Category;
                targetQuestion.Answer = questionUpdateDto.Answers.Select(a => new Answer { Content = a.Content }).ToList();
                targetQuestion.Comments = questionUpdateDto.Comments.Select(c => new Comment { Content = c.Content }).ToList();
                await _questionWriteRepository.SaveAsync();
            }
        }
        public async Task RemoveAsync(string id)
        {
            await _questionWriteRepository.RemoveAsync(id);
            await _questionWriteRepository.SaveAsync();
        }
    }
}
