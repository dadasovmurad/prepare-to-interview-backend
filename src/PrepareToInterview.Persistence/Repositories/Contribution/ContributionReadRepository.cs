using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class ContributionReadRepository : ReadRepository<PrepareToInterview.Domain.Entities.Contribution>, IContributionReadRepository
    {
        public ContributionReadRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}