using AutoMapper;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagCreateDto>().ReverseMap();
            CreateMap<Tag, TagUpdateDto>().ReverseMap();
            CreateMap<Tag, TagListDto>().ReverseMap();
            CreateMap<TagCreateDto, Tag>();
            //CreateMap<CreateQuestionCommand, Question>()
            //.ForMember(dest => dest.QuestionTags, opt => opt.MapFrom(src =>
            //    src.Tags.Select(tag => new QuestionTag { Tag = new Tag { Name = tag.Name } })));

            //CreateMap<Tag,TagListDto>()
        }
    }
}
