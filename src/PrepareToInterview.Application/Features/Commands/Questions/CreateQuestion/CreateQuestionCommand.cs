using AutoMapper;
using MediatR;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.QuestionTranslations;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<IDataResult<QuestionCreatedDto>>
    {
        public int CategoryId { get; set; }
        public string Difficulty { get; set; }
        public List<AnswerCreateDto> Answers { get; set; }
        public List<TagCreateDto> Tags { get; set; }
        public List<QuestionTranslationsCreateDto> QuestionTranslations { get; set; }

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
                await _questionWriteRepository.AddAsync(_mapper.Map<Question>(request));

                await _questionWriteRepository.SaveAsync();

                return new SuccessDataResult<QuestionCreatedDto>();
            }
        }
    }
}