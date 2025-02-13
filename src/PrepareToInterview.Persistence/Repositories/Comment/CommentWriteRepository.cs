using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class CommentWriteRepository : WriteRepository<PrepareToInterview.Domain.Entities.Comment>, ICommentWriteRepository
    {
        public CommentWriteRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}