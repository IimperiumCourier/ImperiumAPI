using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Repository.Context;
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

        public IQueryable<Package> GetAll(DateTime from, DateTime to, Guid placedBy)
        {
            return dbContext.Package.Where(item => item.PlacedBy == placedBy && 
                                                   (item.DateCreated >= from && item.DateCreated <= to));
        }

        public Task<Package?> GetById(Guid id)
        {
            return dbContext.Package.FirstOrDefaultAsync(e => e.Id == id);
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
    }
}
