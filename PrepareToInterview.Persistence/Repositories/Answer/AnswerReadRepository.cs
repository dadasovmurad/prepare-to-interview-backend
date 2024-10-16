using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Persistence.Repositories
{
    public class AnswerReadRepository : ReadRepository<Answer>, IAnswerReadRepository
    {
        public AnswerReadRepository(PrepareToInterviewAPIDbContext context) : base(context) { }
    }
}
