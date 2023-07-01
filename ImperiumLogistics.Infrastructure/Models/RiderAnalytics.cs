using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class RiderAnalytics
    {
        public int TotalPackageAvailableForPickUp { get; set; }
        public int TotalPackagePickedUp { get; set; }
        public int TotalPackageAvailableForDelivery { get; set; }
        public int TotalPackageDelivered { get; set; }
    }
}
