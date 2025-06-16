using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class UserWriteRepository : WriteRepository<PrepareToInterview.Domain.Entities.AppUser>, IUserWriteRepository
    {
        public UserWriteRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}
