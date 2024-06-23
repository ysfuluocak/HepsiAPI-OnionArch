using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiAPI.Application.Wrappers.PageResponse
{
    public class PageResponse<T>
    {
        public int CurrentIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int CurrentCount { get; private set; }
        public List<T> Items { get; private set; }

        public PageResponse(List<T> items, int count, int pageIndex, int pageSize)
        {
            CurrentIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            CurrentCount = HasNextPage ? pageSize : count % pageSize;
            Items = items;
        }

        public bool HasPreviousPage => CurrentIndex > 1;

        public bool HasNextPage => CurrentIndex < TotalPages;
    }
}
