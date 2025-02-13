using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;

namespace PrepareToInterview.Application.Features.Queries.Tags.GetAllTags
{
    public class GetAllTagsQuery : IRequest<IDataResult<IList<TagListDto>>>
    {
        public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, IDataResult<IList<TagListDto>>>
        {
            ITagReadRepository _tagReadRepository;
            IMapper _mapper;

            public GetAllTagsQueryHandler(ITagReadRepository tagReadRepository, IMapper mapper)
            {
                _tagReadRepository = tagReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IList<TagListDto>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
            {
                var tags = await _tagReadRepository.GetAll().ToListAsync();
                var resultData = _mapper.Map<List<TagListDto>>(tags);

                return new SuccessDataResult<IList<TagListDto>>(resultData);
            }
        }
    }
}
