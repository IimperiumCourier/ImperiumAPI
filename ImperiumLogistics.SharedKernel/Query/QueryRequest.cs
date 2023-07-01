using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.Query
{
    public class QueryRequest
    {
        [Required(ErrorMessage = "PagedQuery property must be set")]
        public PagedQueryRequest? PagedQuery { get; set; }
        public SearchFilter? TextFilter { get; set; }
        public DateFilter? DateFilter { get; set; }
    }
}
