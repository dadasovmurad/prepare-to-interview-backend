using MediatR;
using PrepareToInterview.Application.Abstractions.Services;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Question.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommandRequest,IDataResult<CreateQuestionCommandResponse>>
    {
        private readonly IQuestionService _questionService;

        public CreateQuestionCommandHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IDataResult<CreateQuestionCommandResponse>> Handle(CreateQuestionCommandRequest request, CancellationToken cancellationToken)
        {
            await _questionService.AddAsync(new DTOs.Question.QuestionCreateDto
            {
                Content = request.Content,
                Category = request.Category,
                SuitableFor = request.SuitableFor,
                Answers = request.Answers,
                Comments = request.Comments,
            });
            return new SuccessDataResult<CreateQuestionCommandResponse>("Question successfully created !");
        }
    }
}
