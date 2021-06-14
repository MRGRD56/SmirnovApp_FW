﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmirnovApp.Context;

namespace SmirnovApp.Context.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("EstateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EstateId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Estate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<int>("FloorsCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TypeId");

                    b.ToTable("Estates");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.EstateType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstateTypes");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.InitialData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LivingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PassportIssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PassportIssuedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportSeries")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InitialData");

                    b.HasDiscriminator<string>("Discriminator").HasValue("InitialData");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceCategory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Client", b =>
                {
                    b.HasBaseType("SmirnovApp.Model.DbModels.InitialData");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.ToTable("InitialData");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Employee", b =>
                {
                    b.HasBaseType("SmirnovApp.Model.DbModels.InitialData");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Owner", b =>
                {
                    b.HasBaseType("SmirnovApp.Model.DbModels.InitialData");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnName("Owner_ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .HasColumnName("Owner_Phone")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Owners");

                    b.HasDiscriminator().HasValue("Owner");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.User", b =>
                {
                    b.HasBaseType("SmirnovApp.Model.DbModels.InitialData");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServicesDirection")
                        .HasColumnType("int");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.IndividualClient", b =>
                {
                    b.HasBaseType("SmirnovApp.Model.DbModels.Client");

                    b.ToTable("IndividualClients");

                    b.HasDiscriminator().HasValue("IndividualClient");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.LegalEntityClient", b =>
                {
                    b.HasBaseType("SmirnovApp.Model.DbModels.Client");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("LegalEntityClients");

                    b.HasDiscriminator().HasValue("LegalEntityClient");
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Contract", b =>
                {
                    b.HasOne("SmirnovApp.Model.DbModels.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmirnovApp.Model.DbModels.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmirnovApp.Model.DbModels.Estate", "Estate")
                        .WithMany("Contracts")
                        .HasForeignKey("EstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmirnovApp.Model.DbModels.Service", "Service")
                        .WithMany("Contracts")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Estate", b =>
                {
                    b.HasOne("SmirnovApp.Model.DbModels.Owner", "Owner")
                        .WithMany("Estates")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmirnovApp.Model.DbModels.EstateType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmirnovApp.Model.DbModels.Employee", b =>
                {
                    b.HasOne("SmirnovApp.Model.DbModels.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
