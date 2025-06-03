using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.Persistence.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(PrepareToInterviewAPIDbContext context) : base(context)
        {
        }

        // Add any comment-specific method implementations here if needed
    }
} 