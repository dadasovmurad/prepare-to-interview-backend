using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Domain.Entities.Common;
using System.Linq.Expressions;

namespace PrepareToInterview.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        
        // Read operations
        IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null, bool tracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
        T Get(Expression<Func<T, bool>> predicate, bool tracking = true);
        Task<T> GetByIdAsync(int id, bool tracking = true);

        // Write operations
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);
        bool Remove(T model);
        bool RemoveRange(List<T> models);
        Task<bool> RemoveAsync(int id);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
