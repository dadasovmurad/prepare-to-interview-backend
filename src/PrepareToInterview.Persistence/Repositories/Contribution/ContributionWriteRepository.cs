using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class ContributionWriteRepository : WriteRepository<PrepareToInterview.Domain.Entities.Contribution>, IContributionWriteRepository
    {
        public ContributionWriteRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}