using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.AuthAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository.Configuration
{
    public class AuthConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.RefreshToken).IsRequired(false).HasMaxLength(500);
            builder.Property(e => e.RefreshTokenExpiryTime).HasDefaultValue(DateTime.MinValue);
            builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);

            builder.OwnsOne(e => e.Credential, a =>
            {
                a.Property(p => p.LoginAttempt).HasDefaultValue(0).HasColumnName("LoginAttempt");
                a.Property(p => p.LastDateChanged).HasDefaultValue(DateTime.MinValue).HasColumnName("PwdLastDateChanged");
                a.Property(p => p.PasswordHash).IsRequired(false).HasColumnName("Password");
            });
        }
    }
}
