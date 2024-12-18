using MediatR;
using PrepareToInterview.Application.Abstractions.Services;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Features.Queries.Question.GetAllQuestion
{
    public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQueryRequest, IDataResult<GetAllQuestionQueryResponse>>
    {
        private readonly IQuestionService _questionService;

        public GetAllQuestionQueryHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IDataResult<GetAllQuestionQueryResponse>> Handle(GetAllQuestionQueryRequest request, CancellationToken cancellationToken)
        {
            var questions = await _questionService.GetAllAsync();
            return new SuccessDataResult<GetAllQuestionQueryResponse>(new GetAllQuestionQueryResponse { Questions = questions });
        }
    }
}
