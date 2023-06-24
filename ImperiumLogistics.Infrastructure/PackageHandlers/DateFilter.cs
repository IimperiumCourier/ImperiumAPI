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
            var dateFilter = queryRequest.DateFilter;
            if (dateFilter != null && HasValidDates(dateFilter.From, dateFilter.To))
            {
                var _data = data.Where(item => item.DateCreated >= dateFilter.From &&
                                                  item.DateCreated <= dateFilter.To);

                data = _data;
            }
            
            if(successor != null && queryRequest != null)
            {
                successor.Apply(ref data, queryRequest);
            }
        }

        private bool HasValidDates(DateTime? from, DateTime? to)
        {
            if(from == null || from.Value == DateTime.MinValue)
            {
                return false;
            }

            if(to == null || to.Value == DateTime.MinValue)
            {
                return false;
            }

            return true;
        }
    }
}
