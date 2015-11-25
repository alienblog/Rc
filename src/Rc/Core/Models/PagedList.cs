
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rc.Core.Models
{
    public class PagedList<T>
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total")]
        public int TotalCount { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("rows")]
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