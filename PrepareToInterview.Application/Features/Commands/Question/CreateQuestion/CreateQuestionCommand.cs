using MediatR;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Question.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<IDataResult<QuestionCreatedDto>>
    {
        public string Content { get; set; }
        public string Category { get; set; }
        public string SuitableFor { get; set; }
        public List<AnswerCreateDto> Answers { get; set; }

        public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, IDataResult<QuestionCreatedDto>>
        {
            private IQuestionWriteRepository _questionWriteRepository;

            public CreateQuestionCommandHandler(IQuestionWriteRepository questioNWriteRepository)
            {
                _questionWriteRepository = questioNWriteRepository;
            }

            public async Task<IDataResult<QuestionCreatedDto>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
            {
                await _questionWriteRepository.AddAsync(new PrepareToInterview.Domain.Entities.Question
                {
                    Content = request.Content,
                    Category = request.Category,
                    SuitableFor = request.SuitableFor,
                    Answer = request.Answers.Select(x => new Answer { Content = x.Content }).ToList(),
                });
                await _questionWriteRepository.SaveAsync();

                return new SuccessDataResult<QuestionCreatedDto>();
            }
        }
    }
}
