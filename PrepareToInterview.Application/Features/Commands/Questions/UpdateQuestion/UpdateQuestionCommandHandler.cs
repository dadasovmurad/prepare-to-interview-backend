using MediatR;
using PrepareToInterview.Application.Abstractions.Services;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Question.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommandRequest, IDataResult<UpdateQuestionCommandResponse>>
    {
        private readonly IQuestionService _questionService;
        public UpdateQuestionCommandHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IDataResult<UpdateQuestionCommandResponse>> Handle(UpdateQuestionCommandRequest request, CancellationToken cancellationToken)
        {
            await _questionService.UpdateAsync(new DTOs.Question.QuestionUpdateDto
            {
                Id = request.Id,
                Content = request.Content,
                Category = request.Category,
                SuitableFor = request.SuitableFor,
                Answers = request.Answers,
                Comments = request.Comments
            });

            return new SuccessDataResult<UpdateQuestionCommandResponse>("Question successfully updated !");
        }
    }
}
