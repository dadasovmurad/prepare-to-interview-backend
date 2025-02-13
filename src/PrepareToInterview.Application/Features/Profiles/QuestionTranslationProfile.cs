using AutoMapper;
using PrepareToInterview.Application.DTOs.QuestionTranslations;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class QuestionTranslationProfile : Profile
    {
        public QuestionTranslationProfile()
        {
            CreateMap<QuestionTranslation, QuestionTranslationsListDto>().ReverseMap();
        }
    }
}
