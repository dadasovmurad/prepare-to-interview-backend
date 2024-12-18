using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Persistence.Repositories
{
    public class CategoryReadRepository : ReadRepository<PrepareToInterview.Domain.Entities.Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}
