using AutoMapper;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Features.Commands.Categories.CreateCategory;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class CategoryTranslationProfile : Profile
    {
        public CategoryTranslationProfile()
        {
            //CreateMap<CreateQuestionCommand, Question>()
            //.ForMember(dest => dest.QuestionTranslations, opt => opt.MapFrom(src =>
            //    src.QuestionTranslations.Select(tr => new QuestionTranslation { LanguageCode = tr.LanguageCode, Content = tr.Content })));

            //CreateMap<Category, CategoryTranslation>();
            //CreateMap<Category, CategoryTranslationsListDto>()
            // .ReverseMap()
            // .ForMember(
            //     dest => dest.CategoryTranslations,
            //     opt => opt.MapFrom(src => src.CategoryTranslations.Select(tr => new CategoryTranslationsListDto
            //     {
            //         Content = src.CategoryTranslations.FirstOrDefault()!.Content,
            //         LanguageCode = src.CategoryTranslations.FirstOrDefault()!.LanguageCode,
            //     }))
            // );
            CreateMap<CategoryTranslationsListDto, CategoryTranslation>();
            CreateMap<CreateCategoryCommand, Category>()
           .ForMember(
               dest => dest.CategoryTranslations,
               opt => opt.MapFrom(src => src.CategoryTranslations));


               //{
               //Content = src.CategoryTranslations.FirstOrDefault()!.Content,
               //LanguageCode = src.CategoryTranslations.FirstOrDefault()!.LanguageCode,
               //}))
               //);


            //CreateMap<Category, CategoryTranslationsListDto>()
            //    .ForMember(dest => dest, opt => opt.MapFrom(x => x.CategoryTranslations));
        }
    }
}
