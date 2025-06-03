using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Services;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Persistence.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<CommentGetByIdDto>> GetByIdAsync(int id)
        {
            var comment = await _commentRepository.GetAll(c => c.Id == id)
                .Include(c => c.Answer)
                .FirstOrDefaultAsync();

            if (comment is null)
                return new ErrorDataResult<CommentGetByIdDto>("Comment not found");

            var resultData = _mapper.Map<CommentGetByIdDto>(comment);
            return new SuccessDataResult<CommentGetByIdDto>(resultData);
        }

        public async Task<IDataResult<PagedResponse<CommentListDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var comments = _commentRepository.GetAll()
                .Include(c => c.Answer);

            var pagedResult = await comments.GetPageAsync(pageNumber, pageSize);
            var resultData = _mapper.Map<PagedResponse<CommentListDto>>(pagedResult);
            return new SuccessDataResult<PagedResponse<CommentListDto>>(resultData);
        }

        public async Task<IDataResult<PagedResponse<CommentListDto>>> GetCommentsByAnswerIdAsync(int answerId, int pageNumber = 1, int pageSize = 10)
        {
            var comments = _commentRepository.GetAll(c => c.AnswerId == answerId);

            var pagedResult = await comments.GetPageAsync(pageNumber, pageSize);
            var resultData = _mapper.Map<PagedResponse<CommentListDto>>(pagedResult);
            return new SuccessDataResult<PagedResponse<CommentListDto>>(resultData);
        }

        public async Task<IDataResult<CommentCreatedDto>> CreateAsync(CommentCreateDto commentCreateDto)
        {
            var comment = _mapper.Map<Comment>(commentCreateDto);
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveAsync();

            return new SuccessDataResult<CommentCreatedDto>(new CommentCreatedDto(), "Comment successfully created!");
        }

        public async Task<IDataResult<CommentUpdatedDto>> UpdateAsync(CommentUpdateDto commentUpdateDto)
        {
            var comment = await _commentRepository.GetAll(c => c.Id == commentUpdateDto.Id)
                .Include(c => c.Answer)
                .FirstOrDefaultAsync();

            if (comment is null)
                return new ErrorDataResult<CommentUpdatedDto>("Comment not found");

            _mapper.Map(commentUpdateDto, comment);
            _commentRepository.Update(comment);
            await _commentRepository.SaveAsync();

            return new SuccessDataResult<CommentUpdatedDto>(new CommentUpdatedDto(), "Comment successfully updated!");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment is null)
                return new ErrorResult("Comment not found");

            await _commentRepository.RemoveAsync(id);
            await _commentRepository.SaveAsync();

            return new SuccessResult("Comment successfully deleted!");
        }
    }
} 