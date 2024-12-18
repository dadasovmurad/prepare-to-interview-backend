using AutoMapper;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Features.Commands.Categories.CreateCategory;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(q => q.Name, dest => dest
                                                   .MapFrom(src => src.CategoryTranslations
                                                   .Select(x => x.Content)
                                                   .FirstOrDefault()))
                                                   .ReverseMap();

            CreateMap<CategoryTranslation, CategoryTranslationsListDto>();

            //CreateMap<CreateCategoryCommand, Category>()
            //    .ForMember(q => q.CategoryTranslations, dest => dest.MapFrom(src => src.CategoryTranslations));
        }
    }
}
