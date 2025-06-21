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
            CreateMap<AppUser, CreateUserCommand>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<AppUser, UserCreatedDto>().ReverseMap();
        }
    }
}
