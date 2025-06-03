using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        public AnswerRepository(PrepareToInterviewAPIDbContext context) : base(context)
        {
        }

        // Add any answer-specific method implementations here if needed
    }
} 