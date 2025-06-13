using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}