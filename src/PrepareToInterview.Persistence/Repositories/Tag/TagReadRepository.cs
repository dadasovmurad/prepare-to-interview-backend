using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class TagReadRepository : ReadRepository<Tag>, ITagReadRepository
    {
        public TagReadRepository(PrepareToInterviewAPIDbContext context) : base(context)
        {
        }
    }
}
