using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBuddy.Models.Paging;

namespace PrepareToInterview.Application.Extensions
{
    public static class PagingExtensions
    {
        public static async Task<PagedResponse<T>> GetPageAsync<T>(this IQueryable<T> query, int? currentPage, int? pageSize) where T : class
        {
            int count = await query.CountAsync();
            Page paging = new(currentPage ?? 1, pageSize ?? 10, count);

            var data = await query
                             .Skip(paging.Skip)
                             .Take(paging.PageSize)
                             .AsNoTracking()
                             .ToListAsync();

            var result = new PagedResponse<T>(data, paging);

            return result;
        }
    }
}
