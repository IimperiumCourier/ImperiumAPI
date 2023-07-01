using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.AdminFilterHandler
{
    public class TextFilter : AdminFilterHandler
    {
        public override void Apply(ref IQueryable<Admin> data, QueryRequest queryRequest)
        {
            var textFilter = queryRequest.TextFilter;
            if (textFilter != null && !string.IsNullOrWhiteSpace(textFilter.Keyword))
            {
                string keyword = textFilter.Keyword.ToSentenceCase();

                var _data = data.Where(t => t.Email.Address == keyword || t.FullName == keyword || t.PhoneNumber == keyword);

                data = _data;
            }

            if (successor != null && queryRequest != null)
            {
                successor.Apply(ref data, queryRequest);
            }
        }
    }
}
