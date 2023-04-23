using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImperiumLogistics.Domain.CompanyAggregate;

namespace ImperiumLogistics.Infrastructure.Repository.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(e => e.DateCreated).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Address).HasMaxLength(300).IsRequired();
            builder.Property(e => e.City).IsRequired().HasMaxLength(20);
            builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(e => e.State).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);

            builder.OwnsOne(e => e.Owner, a =>
            {
                a.Property(p => p.FullName).IsRequired()
                    .HasColumnName("FullName").HasMaxLength(100);
            });

            builder.OwnsOne(e => e.EmailAddress, a =>
            {
                a.Property(p => p.Address).IsRequired().HasColumnName("Address").HasMaxLength(100);
            });

            builder.OwnsOne(e => e.Credential, a =>
            {
                a.Property(p => p.LoginAttempt).HasDefaultValue(0).HasColumnName("LoginAttempt");
                a.Property(p => p.LastDateChanged).HasDefaultValue(DateTime.MinValue).HasColumnName("PwdLastDateChanged");
                a.Property(p => p.PasswordHash).IsRequired(false).HasColumnName("Password");
            });
        }
    }
}
