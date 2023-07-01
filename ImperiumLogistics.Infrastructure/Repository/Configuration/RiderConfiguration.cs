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
            builder.Property(e => e.RefreshToken).IsRequired(false).HasMaxLength(500);
            builder.Property(e => e.RefreshTokenExpiryTime).HasDefaultValue(DateTime.MinValue);

            builder.OwnsOne(e => e.Credential, a =>
            {
                a.Property(p => p.LoginAttempt).HasDefaultValue(0).HasColumnName("LoginAttempt");
                a.Property(p => p.LastDateChanged).HasDefaultValue(DateTime.MinValue).HasColumnName("PwdLastDateChanged");
                a.Property(p => p.PasswordHash).IsRequired(false).HasColumnName("Password");
            });

            builder.Property(p => p.Email).IsRequired().HasColumnName("EmailAddress").HasMaxLength(100);
        }
    }
}
