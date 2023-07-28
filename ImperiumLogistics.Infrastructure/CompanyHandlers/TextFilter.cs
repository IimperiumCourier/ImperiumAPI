using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.CompanyHandlers
{
    public class TextFilter : CompanyFilterHandler
    {
        public override void Apply(ref IQueryable<Company> data, QueryRequest queryRequest)
        {
            var textFilter = queryRequest.TextFilter;
            if (textFilter != null && !string.IsNullOrWhiteSpace(textFilter.Keyword))
            {
                string keyword = textFilter.Keyword.ToSentenceCase();

                var _data = data.Where(t => t.EmailAddress.Address.Contains(textFilter.Keyword) || t.Name.Contains(keyword) || t.PhoneNumber.Contains(keyword));

                data = _data;
            }

            if (successor != null && queryRequest != null)
            {
                successor.Apply(ref data, queryRequest);
            }
        }
    }
}
