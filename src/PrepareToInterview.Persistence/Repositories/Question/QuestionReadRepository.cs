using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class QuestionReadRepository : ReadRepository<PrepareToInterview.Domain.Entities.Question>, IQuestionReadRepository
    {
        public QuestionReadRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}
