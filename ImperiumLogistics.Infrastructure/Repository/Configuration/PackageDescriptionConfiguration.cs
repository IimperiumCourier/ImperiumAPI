using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.PackageAggregate;

namespace ImperiumLogistics.Infrastructure.Repository.Configuration
{
    public class PackageDescriptionConfiguration : IEntityTypeConfiguration<PackageDescription>
    {
        public void Configure(EntityTypeBuilder<PackageDescription> builder)
        {
            builder.Property(e => e.DateCreated).HasDefaultValue(DateTime.MinValue);
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.Property(e => e.DateUpdated).HasDefaultValue(DateTime.MinValue);
        }
    }
}
