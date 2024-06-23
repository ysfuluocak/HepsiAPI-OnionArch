using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiAPI.Application.Wrappers.PageResponse
{
    public class PageResponse<T> : DataResponse<T>
    {
        public int CurrentIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int CurrentCount { get; private set; }

        public PageResponse(T data, int count, int pageIndex, int pageSize) : base(data)
        {
            CurrentIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            CurrentCount = HasNextPage ? pageSize : count % pageSize;
        }

        public bool HasPreviousPage
        {
            get { return (CurrentIndex > 1); }
        }

        public bool HasNextPage
        {
            get { return (CurrentIndex < TotalPages); }
        }
    }
}
