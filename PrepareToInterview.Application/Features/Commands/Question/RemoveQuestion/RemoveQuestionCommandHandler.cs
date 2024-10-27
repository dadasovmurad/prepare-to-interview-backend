using MediatR;
using PrepareToInterview.Application.Abstractions.Services;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Question.RemoveQuestion
{
    public class RemoveQuestionCommandHandler : IRequestHandler<RemoveQuestionCommandRequest, IDataResult<RemoveQuestionCommandResponse>>
    {
        private readonly IQuestionService _questionService;
        public RemoveQuestionCommandHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IDataResult<RemoveQuestionCommandResponse>> Handle(RemoveQuestionCommandRequest request, CancellationToken cancellationToken)
        {
            await _questionService.RemoveAsync(request.Id);
            return new SuccessDataResult<RemoveQuestionCommandResponse>("Question successfully removed !");
        }
    }
}
