using AutoMapper;
using PrepareToInterview.Application.DTOs.QuestionTranslations;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
