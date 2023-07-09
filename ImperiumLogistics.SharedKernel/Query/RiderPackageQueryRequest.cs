using ImperiumLogistics.SharedKernel.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.Query
{
    public class RiderPackageQueryRequest : QueryRequest
    {
        public PackageStatus Status { get; set; }

        public Guid RiderId { get; set; }
    }
}
