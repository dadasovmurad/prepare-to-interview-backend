﻿using PrepareToInterview.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null, bool tracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
