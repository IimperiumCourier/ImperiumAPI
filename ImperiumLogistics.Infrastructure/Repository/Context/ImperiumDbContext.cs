using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Infrastructure.Repository.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository.Context
{
    public class ImperiumDbContext : DbContext
    {
        public ImperiumDbContext(DbContextOptions<ImperiumDbContext> options)
          : base(options)
        {

        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<PackageDescription> PackageDescription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new PackageConfiguration());
            modelBuilder.ApplyConfiguration(new PackageDescriptionConfiguration());
        }
    }
}
