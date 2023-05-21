﻿// <auto-generated />
using System;
using ImperiumLogistics.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImperiumLogistics.Infrastructure.Migrations
{
    [DbContext(typeof(ImperiumDbContext))]
    [Migration("20230521122504_AddPackageTable")]
    partial class AddPackageTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            modelBuilder.Entity("ImperiumLogistics.Domain.PackageAggregate.Package", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("LastDateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<Guid>("PlacedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("nvarchar(28)");

                    b.HasKey("Id");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("ImperiumLogistics.Domain.PackageAggregate.PackageDescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PackageDescription");
                });

            modelBuilder.Entity("ImperiumLogistics.Domain.CompanyAggregate.Company", b =>
                {
                    b.OwnsOne("ImperiumLogistics.Domain.CompanyAggregate.Credential", "Credential", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("LastDateChanged")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                                .HasColumnName("PwdLastDateChanged");

                            b1.Property<int>("LoginAttempt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValue(0)
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

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FullName");

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

            modelBuilder.Entity("ImperiumLogistics.Domain.PackageAggregate.Package", b =>
                {
                    b.OwnsOne("ImperiumLogistics.Domain.PackageAggregate.PackageAddress", "DeliveryAddress", b1 =>
                        {
                            b1.Property<Guid>("PackageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("LandMark")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("PackageId");

                            b1.ToTable("Package");

                            b1.WithOwner()
                                .HasForeignKey("PackageId");
                        });

                    b.OwnsOne("ImperiumLogistics.Domain.PackageAggregate.PackageAddress", "PickUpAddress", b1 =>
                        {
                            b1.Property<Guid>("PackageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("LandMark")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("PackageId");

                            b1.ToTable("Package");

                            b1.WithOwner()
                                .HasForeignKey("PackageId");
                        });

                    b.OwnsOne("ImperiumLogistics.Domain.PackageAggregate.PackageCusomer", "Cusomer", b1 =>
                        {
                            b1.Property<Guid>("PackageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.HasKey("PackageId");

                            b1.ToTable("Package");

                            b1.WithOwner()
                                .HasForeignKey("PackageId");
                        });

                    b.Navigation("Cusomer")
                        .IsRequired();

                    b.Navigation("DeliveryAddress")
                        .IsRequired();

                    b.Navigation("PickUpAddress")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
