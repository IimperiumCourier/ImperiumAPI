using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.Query
{
    public class PackageQueryRequest
    {
        [Required(ErrorMessage = "PagedQuery property must be set")]
        public PagedQueryRequest? PagedQuery { get; set; }
        public DateFilter? DateFilter { get; set; }
        public SearchFilter? TextFilter { get; set; }

        [Required(ErrorMessage = "Company ID must be set")]
        public Guid ComanyID { get; set; }

        //public bool HasDateFilter() => DateFilter != null;
        //public bool HasSearchFilter() => DateFilter != null;

    }
}
