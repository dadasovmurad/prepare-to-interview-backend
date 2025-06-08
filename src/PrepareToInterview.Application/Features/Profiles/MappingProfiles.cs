using AutoMapper;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Pagination;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
            CreateMap<Category, CategoryGetByIdDto>();
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            // Question mappings
            CreateMap<Question, QuestionDto>();
            CreateMap<Question, QuestionGetByIdDto>();
            CreateMap<Question, QuestionListDto>();
            CreateMap<Question, QuestionRelatedDto>();
            CreateMap<QuestionCreateDto, Question>();
            CreateMap<QuestionUpdateDto, Question>();

            // Answer mappings
            CreateMap<Answer, AnswerDto>();
            CreateMap<Answer, AnswerGetByIdDto>();
            CreateMap<Answer, AnswerListDto>();
            CreateMap<AnswerCreateDto, Answer>();
            CreateMap<AnswerUpdateDto, Answer>();

            // Comment mappings
            CreateMap<Comment, CommentDto>();
            CreateMap<Comment, CommentGetByIdDto>();
            CreateMap<Comment, CommentListDto>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();

            // Tag mappings
            CreateMap<Tag, TagDto>();
            CreateMap<Tag, TagGetByIdDto>();
            CreateMap<Tag, TagListDto>();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<TagUpdateDto, Tag>();

            // Pagination mappings
            CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));
        }
    }
} 