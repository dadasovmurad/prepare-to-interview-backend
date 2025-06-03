using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(PrepareToInterviewAPIDbContext context) : base(context)
        {
        }

        // Add any question-specific method implementations here if needed
    }
} 