using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities.Common;
using PrepareToInterview.Persistence.Contexts;
using System.Linq.Expressions;

namespace PrepareToInterview.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly PrepareToInterviewAPIDbContext _context;

        public Repository(PrepareToInterviewAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        // Read operations
        public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return predicate == null ? query : query.Where(predicate);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate);
        }

        public T Get(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.FirstOrDefault(predicate);
        }

        public async Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == id);
        }

        // Write operations
        public async Task<bool> AddAsync(T model)
        {
            var entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            await Table.AddRangeAsync(models);
            return true;
        }

        public bool Remove(T model)
        {
            var entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var model = await Table.FirstOrDefaultAsync(data => data.Id == id);
            if (model != null)
                return Remove(model);
            return false;
        }

        public bool Update(T model)
        {
            var entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 