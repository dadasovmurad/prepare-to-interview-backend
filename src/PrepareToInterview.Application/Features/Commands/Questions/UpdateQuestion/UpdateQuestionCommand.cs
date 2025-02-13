using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
//using PrepareToInterview.Application.DTOs.QuestionTranslations;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Features.Commands.Questions.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest<IDataResult<QuestionUpdatedDto>>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Difficulty { get; set; }
        //public bool IsLiked {  get; set; }
        public List<AnswerUpdateDto> Answers { get; set; }
        public List<CommentUpdateDto> Comments { get; set; }
        public List<TagUpdateDto> Tags { get; set; }
        //public List<QuestionTranslationsUpdateDto> QuestionTranslations { get; set; }

        public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, IDataResult<QuestionUpdatedDto>>
        {
            private readonly IQuestionWriteRepository _questionWriteRepository;
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;
            public UpdateQuestionCommandHandler(IQuestionWriteRepository questionWriteRepository, IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionWriteRepository = questionWriteRepository;
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }
            public async Task<IDataResult<QuestionUpdatedDto>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
            {
                var targetQuestion = await _questionReadRepository.GetAll(q => q.Id == request.Id)
                                                           .Include(q => q.Answers)
                                                           //.Include(q => q.QuestionTranslations)
                                                           .Include(q => q.Comments)
                                                           .Include(q => q.QuestionTags)
                                                           .ThenInclude(x => x.Tag)
                                                           .FirstOrDefaultAsync();
                if (targetQuestion is not null)
                {
                    _mapper.Map(request, targetQuestion);
                    await _questionWriteRepository.SaveAsync();
                    return new SuccessDataResult<QuestionUpdatedDto>();
                }
                return new ErrorDataResult<QuestionUpdatedDto>();
            }
        }
    }
}