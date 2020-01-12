using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.Api.Models
{
    public class Paging
    {
        public class UrlQuery
        {
            private const int maxPageSize = 100;
            public bool IncludeCount { get; set; } = false;
            public int? PageNumber { get; set; }

            private int _pageSize = 50;
            public int PageSize
            {
                get
                {
                    return _pageSize;
                }
                set
                {
                    _pageSize = (value < maxPageSize) ? value : maxPageSize;
                }
            }

            public string filterColumn { get; set; }
            public string filter { get; set; }
            public string orderBy { get; set; }
            public string orderByDirection { get; set; } = "ascending";

        }
        public class Pagination
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int? TotalRecords { get; set; }
            public int? TotalPages => TotalRecords.HasValue ? (int)Math.Ceiling(TotalRecords.Value / (double)PageSize) : (int?)null;
        }
    }
}
