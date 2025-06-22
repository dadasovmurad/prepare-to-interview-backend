using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Repositories
{
    public interface IUserReadRepository:IReadRepository<AppUser>
    {
        Task<AppUser> GetUserByPassKeyAsync(string userPassKey);
    }
}
