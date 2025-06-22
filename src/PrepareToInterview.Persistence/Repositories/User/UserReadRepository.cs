using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Utilities.Helpers;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<PrepareToInterview.Domain.Entities.AppUser>, IUserReadRepository
    {
        public UserReadRepository(PrepareToInterviewAPIDbContext context) : base(context) { }

        public async Task<AppUser> GetUserByPassKeyAsync(string userPassKey)
        {
            var users = await GetAll().ToListAsync();
            return users.FirstOrDefault(user =>
                HashingHelper.VerifyHash(userPassKey, user.PassKeyHash));
        }
    }
}
