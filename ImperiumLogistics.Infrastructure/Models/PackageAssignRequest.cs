using ImperiumLogistics.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class PackageAssignRequest
    {
        public Guid RiderId { get; set; }
        public Guid PackageId { get; set; }
        public AssignType Type { get; set; }
    }
}
