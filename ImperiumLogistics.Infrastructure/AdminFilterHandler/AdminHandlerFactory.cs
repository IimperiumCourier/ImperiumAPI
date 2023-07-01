using ImperiumLogistics.Infrastructure.PackageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.AdminFilterHandler
{
    public class AdminHandlerFactory
    {
        public static AdminFilterHandler GetAdminFilters()
        {
            var dateFilter = new DateFilter();
            var textFilter = new TextFilter();

            textFilter.SetSuccessor(dateFilter);

            return textFilter;
        }
    }
}
