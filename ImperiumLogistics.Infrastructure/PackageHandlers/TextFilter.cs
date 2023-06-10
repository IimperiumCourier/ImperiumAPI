using Azure;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Infrastructure.PackageHandlers;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Handlers
{
    public class TextFilter : PackageFilterHandler
    {
        public override void Apply(ref IQueryable<Package> data, PackageQueryRequestDTO queryRequest)
        {
            if (queryRequest.TextFilter != null)
            {
                string _desc = queryRequest?.TextFilter?.Keyword?.ToSentenceCase() ?? string.Empty;
                string _keyword = queryRequest?.TextFilter?.Keyword ?? string.Empty;

                var _data = data.Where(t => t.TrackingNumber == _keyword);

                data = _data;
            }
           
            if (successor != null)
            {
                successor.Apply(ref data, queryRequest);
            }
        }
    }
}
