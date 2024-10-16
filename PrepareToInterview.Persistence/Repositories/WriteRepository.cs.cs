using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities.Common;
using PrepareToInterview.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly PrepareToInterviewAPIDbContext _context;

        public WriteRepository(PrepareToInterviewAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<T> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.Entity;
        }
        public async Task AddRangeAsync(IEnumerable<T> datas) => await Table.AddRangeAsync(datas);

        public void Remove(T model)
        {
            Table.Remove(model);
        }

        public async Task RemoveAsync(string id)
        {
            T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            Remove(model);
        }

        public void RemoveRange(List<T> datas) => Table.RemoveRange(datas);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T model) => Table.Update(model);
    }
}
