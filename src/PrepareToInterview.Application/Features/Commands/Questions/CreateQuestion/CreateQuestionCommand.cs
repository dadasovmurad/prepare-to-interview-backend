using AutoMapper;
using MediatR;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Utilities.Helpers;
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
        public int UserId { get; set; }

        //public List<QuestionTranslationsCreateDto> QuestionTranslations { get; set; }

        public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, IResult>
        {
            private readonly IQuestionWriteRepository _questionWriteRepository;
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;
            public CreateQuestionCommandHandler(IQuestionWriteRepository questioNWriteRepository, IMapper mapper, IQuestionReadRepository questionReadRepository)
            {
                _questionWriteRepository = questioNWriteRepository;
                _mapper = mapper;
                _questionReadRepository = questionReadRepository;
            }

            public async Task<IResult> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
            {
                var question = _mapper.Map<Question>(request);

                // Loop until we generate a unique short URL
                string shortUrl;
                do
                {
                    shortUrl = ShortUrlHelper.GenerateShortUrl();
                }
                while (await _questionReadRepository.AnyAsync(q => q.ShortUrl == shortUrl));

                question.ShortUrl = shortUrl;

                await _questionWriteRepository.AddAsync(question);
                await _questionWriteRepository.SaveAsync();

                return new SuccessResult();
            }
        }
    }
}