using System;
using System.Collections.Generic;
using System.Text;

namespace ImperiumLogistics.SharedKernel.Query
{
    public class PagedQueryRequest
    {
        /// <summary>
        /// The page number for the paginated results.  Default is 1.
        /// Setting a number beyond the last page will just return the last page of data.
        /// </summary>
        public string Keyword { get; set; } = string.Empty;
        public int PageNumber { get; set; } = Utility.DefaultPageNumber;

        private int _pageSize = Utility.DefaultPageSize;
        /// <summary>
        /// The number of items returned for the page. A maximum size of 100 is set.  Default is 20.
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = Math.Min(value, Utility.MaxPageSize); }
        }
        
    }
}



