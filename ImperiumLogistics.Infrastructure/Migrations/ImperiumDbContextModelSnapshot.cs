﻿// <auto-generated />
using System;
using ImperiumLogistics.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImperiumLogistics.Infrastructure.Migrations
{
    [DbContext(typeof(ImperiumDbContext))]
    partial class ImperiumDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ImperiumLogistics.Domain.CompanyAggregate.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("DateCreated")
                        .HasMaxLength(20)
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Company", t =>
                        {
                            t.Property("Address")
                                .HasColumnName("Company_Address");
                        });
                });

            modelBuilder.Entity("ImperiumLogistics.Domain.CompanyAggregate.Company", b =>
                {
                    b.OwnsOne("ImperiumLogistics.Domain.CompanyAggregate.Credential", "Credential", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("LastDateChanged")
                                .HasColumnType("datetime2")
                                .HasColumnName("PwdLastDateChanged");

                            b1.Property<int>("LoginAttempt")
                                .HasColumnType("int")
                                .HasColumnName("LoginAttempt");

                            b1.Property<string>("PasswordHash")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Password");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Company");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("ImperiumLogistics.Domain.CompanyAggregate.Email", "EmailAddress", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Address");

                            b1.Property<bool>("IsVerified")
                                .HasColumnType("bit");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Company");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("ImperiumLogistics.Domain.CompanyAggregate.Owner", "Owner", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("LastName");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Company");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.Navigation("Credential")
                        .IsRequired();

                    b.Navigation("EmailAddress")
                        .IsRequired();

                    b.Navigation("Owner")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
