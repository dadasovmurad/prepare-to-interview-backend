using AutoMapper;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Domain.Entities;

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
            //CreateMap<CreateTagCommand, Tag>().ReverseMap();
            //CreateMap<CreateQuestionCommand, Question>()
            //.ForMember(dest => dest.QuestionTags, opt => opt.MapFrom(src =>
            //    src.Tags.Select(tag => new QuestionTag { Tag = new Tag { Name = tag.Name } })));

            //CreateMap<Tag,TagListDto>()
        }
    }
}
}
