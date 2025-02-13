using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class AnswerWriteRepository : WriteRepository<Answer>, IAnswerWriteRepository
    {
        public AnswerWriteRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}
