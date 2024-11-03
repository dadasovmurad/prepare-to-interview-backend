using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Question.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest<IDataResult<QuestionUpdatedDto>>
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string SuitableFor { get; set; }
        public List<AnswerUpdateDto> Answers { get; set; }
        public List<CommentUpdateDto> Comments { get; set; }

        public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, IDataResult<QuestionUpdatedDto>>
        {
            private readonly IQuestionWriteRepository _questionWriteRepository;
            private readonly IQuestionReadRepository _questionReadRepository;

            public UpdateQuestionCommandHandler(IQuestionWriteRepository questionWriteRepository, IQuestionReadRepository questionReadRepository)
            {
                _questionWriteRepository = questionWriteRepository;
                _questionReadRepository = questionReadRepository;
            }
            public async Task<IDataResult<QuestionUpdatedDto>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
            {
                var targetQuestion = await _questionReadRepository.GetAll(q => q.Id == Guid.Parse(request.Id))
                                                           .Include(q => q.Answer)
                                                           .Include(q => q.Comments)
                                                           .FirstOrDefaultAsync();

                if (targetQuestion is not null)
                {
                    targetQuestion.Content = request.Content;
                    targetQuestion.SuitableFor = request.SuitableFor;
                    targetQuestion.Category = request.Category;
                    targetQuestion.Answer = request.Answers.Select(a => new Answer { Content = a.Content }).ToList();
                    targetQuestion.Comments = request.Comments.Select(c => new Comment { Content = c.Content }).ToList();
                    await _questionWriteRepository.SaveAsync();
                    return new SuccessDataResult<QuestionUpdatedDto>();
                }
                return new ErrorDataResult<QuestionUpdatedDto>();
            }
        }
    }
}
