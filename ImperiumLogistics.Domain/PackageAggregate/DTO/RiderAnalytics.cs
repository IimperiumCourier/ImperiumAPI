using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class RiderAnalytics
    {
        public int TotalDeliveryAttempted { get; set; }
        public int TotalDeliverySuccessful { get; set; }
        public int TotalDeliveryFailed { get; set; }
    }
}
