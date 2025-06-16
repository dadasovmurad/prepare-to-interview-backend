using AutoMapper;
using PrepareToInterview.Application.Features.Commands.Users.CreateUser;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Features.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, CreateUserCommand>().ReverseMap();
        }
    }
}
