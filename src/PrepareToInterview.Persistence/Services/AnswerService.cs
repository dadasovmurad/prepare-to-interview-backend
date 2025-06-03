using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Services;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Persistence.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<AnswerGetByIdDto>> GetByIdAsync(int id)
        {
            var answer = await _answerRepository.GetAll(a => a.Id == id)
                .Include(a => a.Question)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync();

            if (answer is null)
                return new ErrorDataResult<AnswerGetByIdDto>("Answer not found");

            var resultData = _mapper.Map<AnswerGetByIdDto>(answer);
            return new SuccessDataResult<AnswerGetByIdDto>(resultData);
        }

        public async Task<IDataResult<PagedResponse<AnswerListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var answers = _answerRepository.GetAll()
                .Include(a => a.Question)
                .Include(a => a.Comments);

            var pagedResult = await answers.GetPageAsync(pageNumber, pageSize);
            var resultData = _mapper.Map<PagedResponse<AnswerListDto>>(pagedResult);
            return new SuccessDataResult<PagedResponse<AnswerListDto>>(resultData);
        }

        public async Task<IDataResult<PagedResponse<AnswerListDto>>> GetAnswersByQuestionIdAsync(int questionId, int pageNumber = 1, int pageSize = 10)
        {
            var answers = _answerRepository.GetAll(a => a.QuestionId == questionId)
                .Include(a => a.Comments);

            var pagedResult = await answers.GetPageAsync(pageNumber, pageSize);
            var resultData = _mapper.Map<PagedResponse<AnswerListDto>>(pagedResult);
            return new SuccessDataResult<PagedResponse<AnswerListDto>>(resultData);
        }

        public async Task<IDataResult<AnswerCreatedDto>> CreateAsync(AnswerCreateDto answerCreateDto)
        {
            var answer = _mapper.Map<Answer>(answerCreateDto);
            await _answerRepository.AddAsync(answer);
            await _answerRepository.SaveAsync();

            return new SuccessDataResult<AnswerCreatedDto>(new AnswerCreatedDto(), "Answer successfully created!");
        }

        public async Task<IDataResult<AnswerUpdatedDto>> UpdateAsync(AnswerUpdateDto answerUpdateDto)
        {
            var answer = await _answerRepository.GetAll(a => a.Id == answerUpdateDto.Id)
                .Include(a => a.Question)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync();

            if (answer is null)
                return new ErrorDataResult<AnswerUpdatedDto>("Answer not found");

            _mapper.Map(answerUpdateDto, answer);
            _answerRepository.Update(answer);
            await _answerRepository.SaveAsync();

            return new SuccessDataResult<AnswerUpdatedDto>(new AnswerUpdatedDto(), "Answer successfully updated!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            if (answer is null)
                return new ErrorResult("Answer not found");

            await _answerRepository.RemoveAsync(id);
            await _answerRepository.SaveAsync();

            return new SuccessResult("Answer successfully deleted!");
        }
    }
} 