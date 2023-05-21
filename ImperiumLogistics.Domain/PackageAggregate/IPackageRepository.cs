using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate
{
    public interface IPackageRepository
    {
        IQueryable<Package> GetAll();
        IQueryable<Package> GetAll(DateTime from, DateTime to, Guid placedBy);
        Task<Package?> GetById(Guid id);
        Task<Package?> GetByTrackNumber(string number);
        Task<Package> Add(PackageDto package);
        void Update(Package package);
        Task<int> Save();
    }
}
