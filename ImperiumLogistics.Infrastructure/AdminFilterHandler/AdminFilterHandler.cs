using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Infrastructure.PackageHandlers;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.AdminFilterHandler
{
    public abstract class AdminFilterHandler
    {
        protected AdminFilterHandler successor;
        public void SetSuccessor(AdminFilterHandler successor)
        {
            this.successor = successor;
        }

        public abstract void Apply(ref IQueryable<Admin> data, QueryRequest queryRequest);
    }
}
