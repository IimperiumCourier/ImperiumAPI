using System;
using System.Collections.Generic;
using System.Text;

namespace ImperiumLogistics.SharedKernel.Query
{

    public class PagedQueryResult<T>
    {
        private List<T> _items;

        public List<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }

        /// <summary>
        /// The total number of items for this query.
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// The total number of pages available.
        /// </summary>
        public int TotalPageCount { get; set; }

        /// <summary>
        /// The number of items to return for each page. This will only return a maximum of 100 items.
        /// </summary>
        public int CurrentPageSize { get; set; }

        /// <summary>
        /// The current page number.
        /// </summary>
        public int CurrentPageNumber { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }
    }
}
