using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class TagWriteRepository : WriteRepository<PrepareToInterview.Domain.Entities.Tag>, ITagWriteRepository
    {
        public TagWriteRepository(PrepareToInterviewAPIDbContext context) : base(context)
        {
        }
    }
}
