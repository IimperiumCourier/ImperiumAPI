using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.CompanyHandlers
{
    public abstract class CompanyFilterHandler
    {
        protected CompanyFilterHandler successor;
        public void SetSuccessor(CompanyFilterHandler successor)
        {
            this.successor = successor;
        }

        public abstract void Apply(ref IQueryable<Company> data, QueryRequest queryRequest);
    }
}
