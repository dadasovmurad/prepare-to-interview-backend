using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class AnswerReadRepository : ReadRepository<Answer>, IAnswerReadRepository
    {
        public AnswerReadRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}
