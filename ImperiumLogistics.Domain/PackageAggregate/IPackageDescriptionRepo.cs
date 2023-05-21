using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate
{
    public interface IPackageDescriptionRepo
    {
        IQueryable<PackageDescription> GetAll();
        Task<PackageDescription> Add(string name);
        void Remove(Guid Id);
        Task<int> Save();
    }
}
