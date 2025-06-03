using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Services;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Persistence.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<TagGetByIdDto>> GetByIdAsync(int id)
        {
            var tag = await _tagRepository.GetAll(t => t.Id == id)
                .Include(t => t.QuestionTags)
                .ThenInclude(qt => qt.Question)
                .FirstOrDefaultAsync();

            if (tag is null)
                return new ErrorDataResult<TagGetByIdDto>("Tag not found");

            var resultData = _mapper.Map<TagGetByIdDto>(tag);
            return new SuccessDataResult<TagGetByIdDto>(resultData);
        }

        public async Task<IDataResult<PagedResponse<TagListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var tags = _tagRepository.GetAll()
                .Include(t => t.QuestionTags)
                .ThenInclude(qt => qt.Question);

            var pagedResult = await tags.GetPageAsync(pageNumber, pageSize);
            var resultData = _mapper.Map<PagedResponse<TagListDto>>(pagedResult);
            return new SuccessDataResult<PagedResponse<TagListDto>>(resultData);
        }

        public async Task<IDataResult<TagCreatedDto>> CreateAsync(TagCreateDto tagCreateDto)
        {
            var tag = _mapper.Map<Tag>(tagCreateDto);
            await _tagRepository.AddAsync(tag);
            await _tagRepository.SaveAsync();

            return new SuccessDataResult<TagCreatedDto>(new TagCreatedDto(), "Tag successfully created!");
        }

        public async Task<IDataResult<TagUpdatedDto>> UpdateAsync(TagUpdateDto tagUpdateDto)
        {
            var tag = await _tagRepository.GetAll(t => t.Id == tagUpdateDto.Id)
                .Include(t => t.QuestionTags)
                .ThenInclude(qt => qt.Question)
                .FirstOrDefaultAsync();

            if (tag is null)
                return new ErrorDataResult<TagUpdatedDto>("Tag not found");

            _mapper.Map(tagUpdateDto, tag);
            _tagRepository.Update(tag);
            await _tagRepository.SaveAsync();

            return new SuccessDataResult<TagUpdatedDto>(new TagUpdatedDto(), "Tag successfully updated!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag is null)
                return new ErrorResult("Tag not found");

            await _tagRepository.RemoveAsync(id);
            await _tagRepository.SaveAsync();

            return new SuccessResult("Tag successfully deleted!");
        }
    }
} 