using AutoMapper;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Features.Commands.Categories.CreateCategory;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryHeaderDto>().ReverseMap();

            //CreateMap<CategoryTranslation, CategoryTranslationsListDto>();

            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            //    .ForMember(q => q.CategoryTranslations, dest => dest.MapFrom(src => src.CategoryTranslations));
        }
    }
}
