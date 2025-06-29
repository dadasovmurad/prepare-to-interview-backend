using AutoMapper;
using PrepareToInterview.Application.DTOs.User;
using PrepareToInterview.Application.Features.Commands.Users.CreateUser;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommand, AppUser>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); // Ignore ImageUrl as it's handled separately
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<AppUser, UserCreatedDto>().ReverseMap();
            CreateMap<AppUser, UserDetailsDto>().ReverseMap();
        }
    }
}
