using ImperiumLogistics.Infrastructure.PackageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.CompanyHandlers
{
    public class CompanyHandlerFactory
    {
        public static CompanyFilterHandler GetCompanyFilters()
        {
            var dateFilter = new DateFilter();
            var textFilter = new TextFilter();

            textFilter.SetSuccessor(dateFilter);

            return textFilter;
        }
    }
}
