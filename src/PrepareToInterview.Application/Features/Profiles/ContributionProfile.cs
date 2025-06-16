using AutoMapper;
using PrepareToInterview.Application.Features.Commands.Contributions.CreateContribution;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Domain.Enums;
using System.Text.Json;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class ContributionProfile : Profile
    {
            public ContributionProfile()
            {
                CreateMap<CreateContributionCommand, Contribution>()
                    .ForMember(dest => dest.Tags, opt => opt.Ignore()) // Ignore for now
                    .AfterMap((src, dest) =>
                    {
                        dest.Tags = src.Tags != null ? JsonSerializer.Serialize(src.Tags) : null;
                    });
            }
    }

}
