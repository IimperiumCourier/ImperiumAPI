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
        public override void Apply(IQueryable<Package> data, PackageQueryRequestDTO queryRequest)
        {
            if (queryRequest.TextFilter != null)
            {
                string keyword = queryRequest?.TextFilter?.Keyword?.ToSentenceCase() ?? string.Empty;
                data = data.Where(e => e.Description.Contains(keyword));
            }
            else if (successor != null)
            {
                successor.Apply(data, queryRequest);
            }
        }
    }
}
