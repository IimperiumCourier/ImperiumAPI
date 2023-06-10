using ImperiumLogistics.Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.PackageHandlers
{
    internal class HandlerFactory
    {

        public static PackageFilterHandler GetPackageFilters()
        {
            var dateFilter = new DateFilter();
            var textFilter = new TextFilter();

            textFilter.SetSuccessor(dateFilter);

            return textFilter;
        }
    }
}
