using Azure;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.PackageHandlers
{
    internal class DateFilter : PackageFilterHandler
    {
        public override void Apply(ref IQueryable<Package> data, PackageQueryRequestDTO queryRequest)
        {
            if (queryRequest.DateFilter != null)
            {
                var _data = data.Where(item => item.DateCreated >= queryRequest.DateFilter.From &&
                                                  item.DateCreated <= queryRequest.DateFilter.To);

                data = _data;
            }
            
            if(successor != null)
            {
                successor.Apply(ref data, queryRequest);
            }
        }
    }
}
