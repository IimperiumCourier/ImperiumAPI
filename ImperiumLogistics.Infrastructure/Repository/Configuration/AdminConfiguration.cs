using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.PackageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository.Configuration
{
    internal class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            builder.OwnsOne(e => e.Email, a =>
            {
                a.Property(p => p.Address).IsRequired().HasColumnName("EmailAddress").HasMaxLength(100);
            });
        }
    }
}
