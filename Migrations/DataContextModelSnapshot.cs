﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using guacactings.Context;

#nullable disable

namespace guacactings.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.2.23128.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("guacactings.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("guacactings.Models.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique()
                        .HasFilter("[EmployeeId] IS NOT NULL");

                    b.ToTable("administrator", (string)null);
                });

            modelBuilder.Entity("guacactings.Models.Document", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("document", (string)null);
                });

            modelBuilder.Entity("guacactings.Models.DocumentType", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("document_type", (string)null);
                });

            modelBuilder.Entity("guacactings.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int?>("SiteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ServiceId");

                    b.HasIndex("SiteId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("employee", (string)null);
                });

            modelBuilder.Entity("guacactings.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("service", (string)null);
                });

            modelBuilder.Entity("guacactings.Models.Site", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("site", (string)null);
                });

            modelBuilder.Entity("guacactings.Models.Address", b =>
                {
                    b.HasOne("guacactings.Models.Administrator", "CreatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("guacactings.Models.Administrator", "UpdatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByAdministrator");

                    b.Navigation("UpdatedByAdministrator");
                });

            modelBuilder.Entity("guacactings.Models.Administrator", b =>
                {
                    b.HasOne("guacactings.Models.Employee", "Employee")
                        .WithOne("Administrator")
                        .HasForeignKey("guacactings.Models.Administrator", "EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("guacactings.Models.Document", b =>
                {
                    b.HasOne("guacactings.Models.Administrator", "CreatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("guacactings.Models.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeId");

                    b.HasOne("guacactings.Models.Employee", "Employee")
                        .WithMany("Documents")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("guacactings.Models.Administrator", "UpdatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByAdministrator");

                    b.Navigation("DocumentType");

                    b.Navigation("Employee");

                    b.Navigation("UpdatedByAdministrator");
                });

            modelBuilder.Entity("guacactings.Models.DocumentType", b =>
                {
                    b.HasOne("guacactings.Models.Administrator", "CreatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("guacactings.Models.Administrator", "UpdatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByAdministrator");

                    b.Navigation("UpdatedByAdministrator");
                });

            modelBuilder.Entity("guacactings.Models.Employee", b =>
                {
                    b.HasOne("guacactings.Models.Address", "Address")
                        .WithMany("Employees")
                        .HasForeignKey("AddressId");

                    b.HasOne("guacactings.Models.Administrator", "CreatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("guacactings.Models.Service", "Service")
                        .WithMany("Employees")
                        .HasForeignKey("ServiceId");

                    b.HasOne("guacactings.Models.Site", "Site")
                        .WithMany("Employees")
                        .HasForeignKey("SiteId");

                    b.HasOne("guacactings.Models.Administrator", "UpdatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("Address");

                    b.Navigation("CreatedByAdministrator");

                    b.Navigation("Service");

                    b.Navigation("Site");

                    b.Navigation("UpdatedByAdministrator");
                });

            modelBuilder.Entity("guacactings.Models.Service", b =>
                {
                    b.HasOne("guacactings.Models.Administrator", "CreatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("guacactings.Models.Administrator", "UpdatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByAdministrator");

                    b.Navigation("UpdatedByAdministrator");
                });

            modelBuilder.Entity("guacactings.Models.Site", b =>
                {
                    b.HasOne("guacactings.Models.Address", "Address")
                        .WithMany("Sites")
                        .HasForeignKey("AddressId");

                    b.HasOne("guacactings.Models.Administrator", "CreatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("guacactings.Models.Administrator", "UpdatedByAdministrator")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("Address");

                    b.Navigation("CreatedByAdministrator");

                    b.Navigation("UpdatedByAdministrator");
                });

            modelBuilder.Entity("guacactings.Models.Address", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Sites");
                });

            modelBuilder.Entity("guacactings.Models.DocumentType", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("guacactings.Models.Employee", b =>
                {
                    b.Navigation("Administrator");

                    b.Navigation("Documents");
                });

            modelBuilder.Entity("guacactings.Models.Service", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("guacactings.Models.Site", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
