using AutoMapper;
using MediatR;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Commands.Tags.CreateTag
{
    public class CreateTagCommand : IRequest<IDataResult<TagCreatedDto>>
    {
        public string Name { get; set; }
        public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, IDataResult<TagCreatedDto>>
        {
            private readonly ITagWriteRepository _tagWriteRepository;
            private readonly IMapper _mapper;

            public CreateTagCommandHandler(ITagWriteRepository tagWriteRepository, IMapper mapper)
            {
                _tagWriteRepository = tagWriteRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<TagCreatedDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
            {
                var tag = _mapper.Map<Tag>(request);
                await _tagWriteRepository.AddAsync(tag);
                await _tagWriteRepository.SaveAsync();

                return new SuccessDataResult<TagCreatedDto>();
            }
        }
    }
}
