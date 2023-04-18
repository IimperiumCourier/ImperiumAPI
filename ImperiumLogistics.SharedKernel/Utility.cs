using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel
{
    public class Utility
    {
        public static DateTime GetNigerianTime() => DateTime.UtcNow.AddHours(1);
    }
}
