using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities.Common;
using PrepareToInterview.Persistence.Contexts;
using System.Linq.Expressions;

namespace PrepareToInterview.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly PrepareToInterviewAPIDbContext _context;

        public ReadRepository(PrepareToInterviewAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public T Get(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return query.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (predicate is not null) query = query.Where(predicate);
            if (!tracking) query = query.AsNoTracking();

            return tracking ? query.AsTracking() : query.AsNoTracking();

        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate);
        }

        public Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            return GetAsync(x => x.Id == id, tracking);
        }
    }
}