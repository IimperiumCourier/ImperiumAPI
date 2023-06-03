using ImperiumLogistics.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class UpdatePackageStatus
    {
        public PackageStatus Status { get; set; }
    }

    public class UpdatePackageUsingTrackingNum : UpdatePackageStatus
    {
        public string TrackingNumber { get; set; }
    }
}
