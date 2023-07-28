using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.RiderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository.Configuration
{
    public class RiderConfiguration : IEntityTypeConfiguration<Rider>
    {
        public void Configure(EntityTypeBuilder<Rider> builder)
        {
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.LicenseNumber).IsRequired().HasMaxLength(100);
            builder.Property(e => e.FrequentLocation).IsRequired().HasMaxLength(200);
            builder.Property(e => e.BikeRegistrationNumber).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).IsRequired().HasColumnName("EmailAddress").HasMaxLength(100);
        }
    }
}
