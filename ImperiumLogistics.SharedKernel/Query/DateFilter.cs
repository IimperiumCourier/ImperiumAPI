using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.Query
{
    public class DateFilter
    {
        public DateTime From { get; set; } = DateTime.MinValue;
        public DateTime To { get; set; } = DateTime.MinValue;
    }
}
