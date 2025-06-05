using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Domain.Enums;

namespace PrepareToInterview.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public QuestionService(
            IQuestionRepository questionRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<QuestionGetByIdDto>> GetByIdAsync(int id)
        {
            var question = await _questionRepository.GetAll(q => q.Id == id)
                .Include(q => q.Answers)
                .Include(q => q.QuestionTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync();

            if (question is null)
                return new ErrorDataResult<QuestionGetByIdDto>("Question not found");

            var resultData = _mapper.Map<QuestionGetByIdDto>(question);
            return new SuccessDataResult<QuestionGetByIdDto>(resultData);
        }

        public async Task<IDataResult<PagedResponse<QuestionListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var questions = _questionRepository.GetAll()
                .Include(q => q.Category)
                .Include(t => t.QuestionTags)
                .ThenInclude(t => t.Tag);

            var pagedResult = await questions.GetPageAsync(pageNumber, pageSize);
            var resultData = _mapper.Map<PagedResponse<QuestionListDto>>(pagedResult);
            return new SuccessDataResult<PagedResponse<QuestionListDto>>(resultData);
        }

        public async Task<IDataResult<PagedResponse<QuestionListDto>>> GetFilteredQuestionsAsync(
            string? category = null, 
            string? subCategory = null, 
            string? difficulty = null, 
            string[]? tags = null, 
            string? term = null, 
            int pageNumber = 1, 
            int pageSize = 10)
        {
            var questions = _questionRepository.GetAll()
                .Include(q => q.Category)
                .Include(t => t.QuestionTags)
                .ThenInclude(t => t.Tag)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                questions = questions.Where(x => x.Category.Parent == null && x.Category.Name == category);
            }
            if (!string.IsNullOrWhiteSpace(subCategory))
            {
                questions = questions.Where(x => x.Category.Parent != null && x.Category.Name == subCategory);
            }
            if (!string.IsNullOrEmpty(difficulty) && Enum.TryParse<Difficulty>(difficulty, true, out var difficultyEnum))
            {
                questions = questions.Where(x => x.Difficulty == difficultyEnum);
            }
            if (tags is not null && tags.Any())
            {
                questions = questions.Where(x => x.QuestionTags.Any(q => tags.Contains(q.Tag.Name)));
            }
            if (!string.IsNullOrWhiteSpace(term))
            {
                questions = questions.Where(x => x.Content.ToLower().Contains(term.ToLower()));
            }

            var pagedResult = await questions.GetPageAsync(pageNumber, pageSize);
            var resultData = _mapper.Map<PagedResponse<QuestionListDto>>(pagedResult);
            return new SuccessDataResult<PagedResponse<QuestionListDto>>(resultData);
        }

        public async Task<IDataResult<List<QuestionRelatedDto>>> GetRelatedQuestionsAsync(int questionId)
        {
            var question = await _questionRepository.GetAll(x => x.Id == questionId)
                .Include(q => q.QuestionTags)
                .ThenInclude(q => q.Tag)
                .FirstOrDefaultAsync();

            if (question is null)
                return new ErrorDataResult<List<QuestionRelatedDto>>("Question not found!");

            var tagIds = question.QuestionTags.Select(x => x.TagID).ToList();

            var relatedQuestions = await _questionRepository.GetAll()
                .Include(q => q.QuestionTags)
                .ThenInclude(q => q.Tag)
                .Where(x => x.Id != questionId && x.QuestionTags.Any(qt => tagIds.Contains(qt.TagID)))
                .Take(5)
                .ToListAsync();

            var resultData = _mapper.Map<List<QuestionRelatedDto>>(relatedQuestions);
            return new SuccessDataResult<List<QuestionRelatedDto>>(resultData);
        }

        public async Task<IDataResult<int>> GetTotalCountAsync()
        {
            var count = await _questionRepository.GetAll().CountAsync();
            return new SuccessDataResult<int>(count);
        }

        public async Task<IDataResult<QuestionCreatedDto>> CreateAsync(QuestionCreateDto questionCreateDto)
        {
            var question = _mapper.Map<Question>(questionCreateDto);
            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveAsync();

            return new SuccessDataResult<QuestionCreatedDto>(new QuestionCreatedDto(), "Question successfully created!");
        }

        public async Task<IDataResult<QuestionUpdatedDto>> UpdateAsync(QuestionUpdateDto questionUpdateDto)
        {
            var question = await _questionRepository.GetAll(q => q.Id == questionUpdateDto.Id)
                .Include(q => q.Answers)
                .Include(q => q.QuestionTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync();

            if (question is null)
                return new ErrorDataResult<QuestionUpdatedDto>("Question not found");

            _mapper.Map(questionUpdateDto, question);
            _questionRepository.Update(question);
            await _questionRepository.SaveAsync();

            return new SuccessDataResult<QuestionUpdatedDto>(new QuestionUpdatedDto(), "Question successfully updated!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question is null)
                return new ErrorResult("Question not found");

            await _questionRepository.RemoveAsync(id);
            await _questionRepository.SaveAsync();

            return new SuccessResult("Question successfully deleted!");
        }
    }
} 