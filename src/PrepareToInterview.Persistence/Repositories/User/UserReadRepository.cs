using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<PrepareToInterview.Domain.Entities.AppUser>, IUserReadRepository
    {
        public UserReadRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}
