using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Domain.RiderAggregate;
using ImperiumLogistics.Infrastructure.PackageHandlers;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.RiderHandlers
{
    public abstract class RiderFilterHandler
    {
        protected RiderFilterHandler successor;
        public void SetSuccessor(RiderFilterHandler successor)
        {
            this.successor = successor;
        }

        public abstract void Apply(ref IQueryable<Rider> data, QueryRequest queryRequest);
    }
}
