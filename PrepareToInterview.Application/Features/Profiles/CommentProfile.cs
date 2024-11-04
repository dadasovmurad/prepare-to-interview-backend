using AutoMapper;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentUpdateDto>().ReverseMap();
            CreateMap<Comment, CommentListDto>().ReverseMap();
        }
    }
}
