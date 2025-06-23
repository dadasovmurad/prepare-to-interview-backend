using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Domain.Entities.Common;
using System.Linq.Expressions;

namespace PrepareToInterview.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null, bool tracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
        T Get(Expression<Func<T, bool>> predicate, bool tracking = true);
        Task<T> GetByIdAsync(int id, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
