using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class CategoryReadRepository : ReadRepository<PrepareToInterview.Domain.Entities.Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}
