using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class PackageAnalytics
    {
        public int TotalPackageAvailableForPickup { get; set; }
        public int TotalPackageAtWareHouse { get; set; }
        public int TotalPackageDelivered { get; set; }
        public int TotalPackageUnDelivered { get; set; }
    }
}
