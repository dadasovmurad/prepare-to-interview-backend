using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Persistence.Repositories
{
    public class CommentWriteRepository : WriteRepository<PrepareToInterview.Domain.Entities.Comment>, ICommentWriteRepository
    {
        public CommentWriteRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}