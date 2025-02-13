using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
