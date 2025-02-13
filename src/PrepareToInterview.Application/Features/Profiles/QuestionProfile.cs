using AutoMapper;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;
using PrepareToInterview.Application.Features.Commands.Questions.UpdateQuestion;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, CreateQuestionCommand>().ReverseMap();
            CreateMap<Question, UpdateQuestionCommand>().ReverseMap();

            CreateMap<Question, QuestionListDto>();

            CreateMap<Question, QuestionGetByIdDto>().ForMember(q => q.Tags, dest => dest.MapFrom(src => src.QuestionTags.Select(x => x.Tag)));

            CreateMap<PagedResponse<Question>, PagedResponse<QuestionListDto>>().ReverseMap();


            CreateMap<CreateQuestionCommand, Question>()
            .ForMember(dest => dest.QuestionTags, opt => opt.MapFrom(src =>
                src.Tags.Select(tag => new QuestionTag { Tag = new Tag { Name = tag.Name } })));

            CreateMap<UpdateQuestionCommand, Question>()
            .ForMember(dest => dest.QuestionTags, opt => opt.MapFrom(src =>
                 src.Tags.Select(tag => new QuestionTag { Tag = new Tag { Name = tag.Name } })));

            CreateMap<CreateQuestionCommand, Question>();

            CreateMap<UpdateQuestionCommand, Question>();
        }
    }
}