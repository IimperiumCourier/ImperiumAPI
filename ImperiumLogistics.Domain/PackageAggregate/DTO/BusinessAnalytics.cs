using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate.DTO
{
    public class BusinessAnalytics
    {
        public int PackageAvailableForPickUp { get; set; }
        public int PackageAtWareHouse { get; set; }
        public int PackageDelivered { get; set; }
        public int PackageUnDelivered { get; set; }
    }
}
