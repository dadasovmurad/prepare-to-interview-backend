using MediatR;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Features.Commands.Questions.RemoveQuestion
{
    public class RemoveQuestionCommand : IRequest<IDataResult<RemovedQuestionDto>>
    {
        public int Id { get; set; }

        public class RemoveQuestionCommandHandler : IRequestHandler<RemoveQuestionCommand, IDataResult<RemovedQuestionDto>>
        {
            private readonly IQuestionWriteRepository _questionWriteRepository;
            private readonly IQuestionReadRepository _questionReadRepository;

            public RemoveQuestionCommandHandler(IQuestionWriteRepository questionWriteRepository, IQuestionReadRepository questionReadRepository)
            {
                _questionWriteRepository = questionWriteRepository;
                _questionReadRepository = questionReadRepository;
            }
            public async Task<IDataResult<RemovedQuestionDto>> Handle(RemoveQuestionCommand request, CancellationToken cancellationToken)
            {
                await _questionWriteRepository.RemoveAsync(request.Id);
                await _questionWriteRepository.SaveAsync();

                return new SuccessDataResult<RemovedQuestionDto>();
            }
        }
    }
}
