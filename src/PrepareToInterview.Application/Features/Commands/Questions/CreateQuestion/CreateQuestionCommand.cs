using AutoMapper;
using MediatR;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<IDataResult<QuestionCreatedDto>>
    {
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string Difficulty { get; set; }
        public List<AnswerCreateDto> Answers { get; set; }
        public List<TagCreateDto> Tags { get; set; }
        //public List<QuestionTranslationsCreateDto> QuestionTranslations { get; set; }

        public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, IDataResult<QuestionCreatedDto>>
        {
            private readonly IQuestionWriteRepository _questionWriteRepository;
            private readonly IMapper _mapper;
            public CreateQuestionCommandHandler(IQuestionWriteRepository questioNWriteRepository, IMapper mapper)
            {
                _questionWriteRepository = questioNWriteRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<QuestionCreatedDto>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
            {
                var dt = _mapper.Map<Question>(request);
                await _questionWriteRepository.AddAsync(dt);

                await _questionWriteRepository.SaveAsync();

                return new SuccessDataResult<QuestionCreatedDto>();
            }
        }
    }
}