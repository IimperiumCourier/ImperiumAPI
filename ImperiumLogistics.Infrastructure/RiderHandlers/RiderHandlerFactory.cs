using ImperiumLogistics.Infrastructure.PackageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.RiderHandlers
{
    public class RiderHandlerFactory
    {
        public static RiderFilterHandler GetAdminFilters()
        {
            var dateFilter = new DateFilter();
            var textFilter = new TextFilter();

            textFilter.SetSuccessor(dateFilter);

            return textFilter;
        }
    }
}
