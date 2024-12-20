using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Persistence.Repositories
{
    public class TagWriteRepository : WriteRepository<PrepareToInterview.Domain.Entities.Tag>, ITagWriteRepository
    {
        public TagWriteRepository(PrepareToInterviewAPIDbContext context) : base(context)
        {
        }
    }
}
