using AutoMapper;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;
using PrepareToInterview.Application.Features.Commands.Questions.UpdateQuestion;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBuddy.Models.Paging;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, CreateQuestionCommand>().ReverseMap();
            CreateMap<Question, UpdateQuestionCommand>().ReverseMap();
            CreateMap<Question, QuestionGetByIdDto>().ReverseMap();
            CreateMap<Question, QuestionListDto>().ReverseMap();
            CreateMap<PagedResponse<Question>, PagedResponse<QuestionListDto>>().ReverseMap();
        }
    }
}