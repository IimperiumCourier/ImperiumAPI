using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.SharedKernel.Enums;
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
        IQueryable<Package> GetAllByCompanyID(Guid placedBy);
        IQueryable<Package> GetAll();
        Task<Package?> GetById(Guid id);
        Task<Package?> GetByTrackNumber(string number);
        Task<Package> Add(PackageDto package);
        void Update(Package package);
        Task<int> Save();
        IQueryable<Package> GetAllByRiderId(Guid riderId, PackageStatus packageStatus);
        Task<BusinessAnalytics> GetBusinessAnalyticsAsync(Guid businessId);
    }
}
