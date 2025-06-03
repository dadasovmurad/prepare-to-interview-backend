using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(PrepareToInterviewAPIDbContext context) : base(context)
        {
        }

        // Add any tag-specific method implementations here if needed
    }
} 