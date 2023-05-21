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
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.Property(e => e.DateCreated).HasDefaultValue(DateTime.MinValue);
            builder.Property(e => e.Description).HasMaxLength(300).IsRequired();
            builder.Property(e => e.Status).IsRequired().HasMaxLength(100);
            builder.Property(e => e.PlacedBy).IsRequired();
            builder.Property(e => e.TrackingNumber).IsRequired().HasMaxLength(28);
            builder.Property(e => e.LastDateUpdated).HasDefaultValue(DateTime.MinValue);

            builder.OwnsOne(e => e.DeliveryAddress, a =>
            {
                a.Property(p => p.State).IsRequired().HasMaxLength(100);
                a.Property(p => p.City).IsRequired().HasMaxLength(100);
                a.Property(p => p.LandMark).IsRequired().HasMaxLength(500);
                a.Property(p => p.Address).IsRequired().HasMaxLength(100);
            });

            builder.OwnsOne(e => e.Cusomer, a =>
            {
                a.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                a.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                a.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
            });

            builder.OwnsOne(e => e.PickUpAddress, a =>
            {
                a.Property(p => p.State).IsRequired().HasMaxLength(100);
                a.Property(p => p.City).IsRequired().HasMaxLength(100);
                a.Property(p => p.LandMark).IsRequired().HasMaxLength(500);
                a.Property(p => p.Address).IsRequired().HasMaxLength(100);
            });
        }
    }
}
