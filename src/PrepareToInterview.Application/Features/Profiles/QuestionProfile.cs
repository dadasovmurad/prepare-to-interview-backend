using AutoMapper;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.DTOs.User;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;
using PrepareToInterview.Application.Features.Commands.Questions.UpdateQuestion;
using PrepareToInterview.Application.Features.Queries.Questions.GetQuestionByShortUrl;
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

            CreateMap<Question, QuestionListDto>()
                .ForMember(u => u.UserDetails, dest => dest.MapFrom(src => src.User))
                .ReverseMap();

            CreateMap<Question, QuestionRelatedDto>()
                .ForMember(u => u.UserDetails, dest => dest.MapFrom(src => src.User));

            CreateMap<Question, QuestionGetByIdDto>()
                .ForMember(q => q.Title, dest => dest.MapFrom(src => src.Content))
                .ForMember(q => q.Tags, dest => dest.MapFrom(src => src.QuestionTags.Select(x => x.Tag)))
                .ForMember(u => u.UserDetails, dest => dest.MapFrom(src => src.User))
                .ForMember(q => q.Content, dest => dest.MapFrom(src => src.Answers.FirstOrDefault().Content));

            CreateMap<Question, QuestionGetByShortUrlDto>()
                .ForMember(q => q.Title, dest => dest.MapFrom(src => src.Content))
                .ForMember(q => q.Tags, dest => dest.MapFrom(src => src.QuestionTags.Select(x => x.Tag)))
                .ForMember(u => u.UserDetails, dest => dest.MapFrom(src => src.User))
                .ForMember(q => q.Content, dest => dest.MapFrom(src => src.Answers.FirstOrDefault().Content));

            CreateMap<PagedResponse<Question>, PagedResponse<QuestionListDto>>().ReverseMap();

            CreateMap<CreateQuestionCommand, Question>()
            .ForMember(dest => dest.QuestionTags, opt => opt.MapFrom(src =>
                src.Tags.Select(tag => new QuestionTag { Tag = new Tag { Name = tag.Name } })));

            CreateMap<UpdateQuestionCommand, Question>()
            .ForMember(dest => dest.QuestionTags, opt => opt.MapFrom(src =>
                 src.Tags.Select(tag => new QuestionTag { Tag = new Tag { Name = tag.Name } })));

            //CreateMap<CreateQuestionCommand, Question>();

            CreateMap<UpdateQuestionCommand, Question>();

        }
    }
}