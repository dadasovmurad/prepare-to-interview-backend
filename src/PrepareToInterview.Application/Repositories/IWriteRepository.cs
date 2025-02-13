using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T model);
        Task AddRangeAsync(IEnumerable<T> datas);
        void RemoveRange(List<T> datas);
        void Remove(T model);
        Task RemoveAsync(int id);
        T Update(T model);
        Task SaveAsync();
    }
}