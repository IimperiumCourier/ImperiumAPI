using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.Infrastructure.Repository.Context;
using ImperiumLogistics.SharedKernel.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository
{
    public class PackageRepository: RepositoryAbstract,IPackageRepository
    {
        private ImperiumDbContext dbContext;
        public PackageRepository(ImperiumDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Package> Add(PackageDto package)
        {
            var _package = Package.GetPackage(package);
            dbContext.Package.AddAsync(_package);

            return Task.FromResult(_package);
        }

        public IQueryable<Package> GetAll()
        {
            return from package in dbContext.Package select package;
        }

        public IQueryable<Package> GetAllByCompanyID(Guid placedBy)
        {
            return dbContext.Package.Where(item => item.PlacedBy == placedBy);
        }

        public Task<Package?> GetById(Guid id)
        {
            return dbContext.Package.FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<Package> GetListByIds(List<Guid> id)
        {
            return dbContext.Package.Where(e => id.Contains(e.Id));
        }

        public Task<Package?> GetByTrackNumber(string number)
        {
            return dbContext.Package.FirstOrDefaultAsync(e => e.TrackingNumber == number);
        }

        public Task<int> Save()
        {
            return dbContext.SaveChangesAsync();
        }

        public void Update(Package package)
        {
            dbContext.Package.Update(package);
        }

        public void UpdateList(List<Package> packages)
        {
            dbContext.Package.UpdateRange(packages);
        }

        public IQueryable<Package> GetAllByRiderId(Guid riderId, PackageStatus packageStatus)
        {
            string _status = packageStatus.GetString();

            return dbContext.Package.Where(item => (item.PickupRider.RiderId == riderId 
                                                    || item.DeliveryRider.RiderId == riderId) 
                                                    && item.Status == _status );
        }

        public async Task<RiderAnalytics> GetRiderAnalyticsAsync(Guid riderId)
        {
            string unDeliveredStatus  = PackageStatus.UnDelivered.GetString();
            string deliveredStatus = PackageStatus.Delivered.GetString();



            var unDeliveredCount = await dbContext.Package.CountAsync(e => e.PickupRider.RiderId == riderId &&
                                                                      e.Status == unDeliveredStatus);
            var deliveredCount = await dbContext.Package.CountAsync(e => e.DeliveryRider.RiderId == riderId &&
                                                                      e.Status == deliveredStatus);

            return new RiderAnalytics
            {
                TotalDeliveryAttempted = deliveredCount + unDeliveredCount,
                TotalDeliveryFailed = unDeliveredCount,
                TotalDeliverySuccessful = deliveredCount
            };
        }

        public async Task<BusinessAnalytics> GetBusinessAnalyticsAsync(Guid businessId)
        {
            string availForPickUpStatus = PackageStatus.AvailableForPickUp.GetString();
            string wareHouseStatus = PackageStatus.WareHouse.GetString();
            string unDeliveredStatus = PackageStatus.UnDelivered.GetString();
            string deliveredStatus = PackageStatus.Delivered.GetString();



            var pickUpCount = await dbContext.Package.CountAsync(e => e.PlacedBy == businessId &&
                                                                      e.Status == availForPickUpStatus);
            var deliveredCount = await dbContext.Package.CountAsync(e => e.PlacedBy == businessId &&
                                                                      e.Status == deliveredStatus);
            var wareHouseCount = await dbContext.Package.CountAsync(e => e.PlacedBy == businessId &&
                                                                      e.Status == wareHouseStatus);
            var unDeliveredCount = await dbContext.Package.CountAsync(e => e.PlacedBy == businessId &&
                                                                      e.Status == unDeliveredStatus);

            return new BusinessAnalytics
            {
                PackageAtWareHouse = wareHouseCount,
                PackageAvailableForPickUp = pickUpCount,
                PackageDelivered= deliveredCount,
                PackageUnDelivered = unDeliveredCount
            };
        }

        public async Task<PackageAnalytics> GetPackageAnalyticsAsync()
        {
            string availForPickUpStatus = PackageStatus.AvailableForPickUp.GetString();
            string wareHouseStatus = PackageStatus.WareHouse.GetString();
            string unDeliveredStatus = PackageStatus.UnDelivered.GetString();
            string deliveredStatus = PackageStatus.Delivered.GetString();



            var pickUpCount = await dbContext.Package.CountAsync(e => e.Status == availForPickUpStatus);
            var unDeliveredCount = await dbContext.Package.CountAsync(e => e.Status == unDeliveredStatus);
            var wareHouseCount = await dbContext.Package.CountAsync(e => e.Status == wareHouseStatus);
            var deliveredCount = await dbContext.Package.CountAsync(e => e.Status == deliveredStatus);

            return new PackageAnalytics
            {
                TotalPackageAtWareHouse = wareHouseCount,
                TotalPackageAvailableForPickup = pickUpCount,
                TotalPackageDelivered = deliveredCount,
                TotalPackageUnDelivered = unDeliveredCount
            };
        }
    }
}
