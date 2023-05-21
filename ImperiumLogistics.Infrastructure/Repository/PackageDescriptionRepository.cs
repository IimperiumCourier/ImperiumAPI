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
    public class PackageDescriptionRepository: RepositoryAbstract,IPackageDescriptionRepo
    {
        private ImperiumDbContext dbContext;
        public PackageDescriptionRepository(ImperiumDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<PackageDescription> Add(string name)
        {
            var _packageDesc = PackageDescription.CreatePackageDescription(name);
            dbContext.PackageDescription.AddAsync(_packageDesc);

            return Task.FromResult(_packageDesc);
        }

        public IQueryable<PackageDescription> GetAll()
        {
            return from packageDesc in dbContext.PackageDescription 
                   where packageDesc.IsDeleted == false
                   select packageDesc;
        }

        public Task<PackageDescription?> GetById(Guid id)
        {
            return dbContext.PackageDescription.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<int> Save()
        {
            return dbContext.SaveChangesAsync();
        }

        public void Remove(Guid Id)
        {
            var _desc = dbContext.PackageDescription.FirstOrDefault(e => e.Id == Id);

            dbContext.PackageDescription.Update(_desc);
        }
    }
}
