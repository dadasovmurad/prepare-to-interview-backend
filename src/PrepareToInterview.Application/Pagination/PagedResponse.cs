using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Pagination
{
    public class PagedResponse<T>
    {
        public IList<T> Items { get; set; }

        public Page PageInfo { get; set; }

        public PagedResponse(IList<T> items, Page pageInfo)
        {
            Items = items;
            PageInfo = pageInfo;
        }

        public PagedResponse()
            : this((IList<T>)new List<T>(), new Page())
        {
        }
    }
}
