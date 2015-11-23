
using System.Collections.Generic;

namespace Rc.Core.Models
{
    public class PagedList<T>
    {
        public int Page { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public IList<T> Rows { get; set; }

        public PagedList()
        { }

        public bool HasNextPage
        {
            get
            {
                return TotalCount > Page * PageSize;
            }
        }
		
		public bool HasPrevPage
		{
			get
			{
				return Page > 1;
			}
		}

        public PagedList(IList<T> data, int pageSize, int page, int totalCount)
        {
            Rows = data;
            PageSize = pageSize;
            Page = page;
            TotalCount = totalCount;
        }
    }
}