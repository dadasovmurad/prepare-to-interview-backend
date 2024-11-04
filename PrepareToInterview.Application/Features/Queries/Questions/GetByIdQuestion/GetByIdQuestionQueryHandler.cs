using MediatR;
using PrepareToInterview.Application.Abstractions.Services;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Question.GetByIdQuestion
{
    public class GetByIdQuestionQueryHandler : IRequestHandler<GetByIdQuestionQueryRequest, IDataResult<GetByIdQuestionQueryResponse>>
    {
        private readonly IQuestionService _questionService;

        public GetByIdQuestionQueryHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IDataResult<GetByIdQuestionQueryResponse>> Handle(GetByIdQuestionQueryRequest request, CancellationToken cancellationToken)
        {
            var responseQuestion = await _questionService.GetByIdAsync(request.Id);
            var response = new GetByIdQuestionQueryResponse()
            {
                Id = request.Id,
                Content = responseQuestion.Content,
                SuitableFor = responseQuestion.SuitableFor,
                Answers = responseQuestion.Answers,
                Category = responseQuestion.Category,
                Comments = responseQuestion.Comments
            };
            return new SuccessDataResult<GetByIdQuestionQueryResponse>(response);
        }
    }
}
