using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.PackageHandlers
{
    public abstract class PackageFilterHandler
    {
        protected PackageFilterHandler successor;
        public void SetSuccessor(PackageFilterHandler successor)
        {
            this.successor = successor;
        }
        public abstract void Apply(ref IQueryable<Package> data, PackageQueryRequestDTO queryRequest);
    }
}
