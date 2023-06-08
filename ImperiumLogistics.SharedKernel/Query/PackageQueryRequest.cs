using AutoMapper.Configuration.Annotations;
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
    }

    public class PackageQueryRequestDTO : PackageQueryRequest
    {
        public Guid ComanyID { get; set; }
    }

}
