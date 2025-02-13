using AutoMapper;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerListDto>();
            CreateMap<Answer, AnswerCreateDto>().ReverseMap();
            CreateMap<Answer, AnswerUpdateDto>().ReverseMap();
        }
    }
}
