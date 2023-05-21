using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class PackageQueryRequest
    {
        public DateTime From { get; set; } = DateTime.MinValue;
        public DateTime To { get; set; } = DateTime.MinValue;
        public Guid ComanyID { get; set; }

        public bool UseDateForFilter()
        {
            bool isGreaterThanMinVal = From > DateTime.MinValue && To > DateTime.MinValue;
            bool isLesserThanMaxVal = From < DateTime.MaxValue && To < DateTime.MaxValue;

            return isGreaterThanMinVal && isLesserThanMaxVal;
        }
    }
}
