﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project5.Model;

namespace Project5.Migrations
{
    [DbContext(typeof(Database_FinesContext))]
    [Migration("20210702084114_init002")]
    partial class init002
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project5.Model.Car", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.HasKey("Id");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("Project5.Model.Fine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CarId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateTmeOfAccident")
                        .HasColumnType("datetime");

                    b.Property<long?>("DriverId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPersonal")
                        .HasColumnType("bit");

                    b.Property<long>("RegistratorId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("DriverId");

                    b.HasIndex("RegistratorId");

                    b.ToTable("Fine");
                });

            modelBuilder.Entity("Project5.Model.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength(true);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.Property<long?>("PersonCarsId")
                        .HasColumnType("bigint");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength(true);

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Project5.Model.PersonCar", b =>
                {
                    b.Property<long>("CarId")
                        .HasColumnType("bigint");

                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime");

                    b.HasKey("CarId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonCars");
                });

            modelBuilder.Entity("Project5.Model.Registrator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GerNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long?>("PersonId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Registrator");
                });

            modelBuilder.Entity("Project5.Model.Fine", b =>
                {
                    b.HasOne("Project5.Model.Car", "Car")
                        .WithMany("Fines")
                        .HasForeignKey("CarId")
                        .HasConstraintName("FK_Fine_Car");

                    b.HasOne("Project5.Model.Person", "Driver")
                        .WithMany("Fines")
                        .HasForeignKey("DriverId")
                        .HasConstraintName("FK_Fine_Person");

                    b.HasOne("Project5.Model.Registrator", "Registrator")
                        .WithMany("Fines")
                        .HasForeignKey("RegistratorId")
                        .HasConstraintName("FK_Fine_Registrator")
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Driver");

                    b.Navigation("Registrator");
                });

            modelBuilder.Entity("Project5.Model.PersonCar", b =>
                {
                    b.HasOne("Project5.Model.Car", "Car")
                        .WithMany("PersonCars")
                        .HasForeignKey("CarId")
                        .HasConstraintName("FK_PersonCars_Car")
                        .IsRequired();

                    b.HasOne("Project5.Model.Person", "Person")
                        .WithMany("PersonCars")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK_PersonCars_Person")
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Project5.Model.Registrator", b =>
                {
                    b.HasOne("Project5.Model.Person", "Person")
                        .WithMany("Registrators")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK_Registrator_Person");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Project5.Model.Car", b =>
                {
                    b.Navigation("Fines");

                    b.Navigation("PersonCars");
                });

            modelBuilder.Entity("Project5.Model.Person", b =>
                {
                    b.Navigation("Fines");

                    b.Navigation("PersonCars");

                    b.Navigation("Registrators");
                });

            modelBuilder.Entity("Project5.Model.Registrator", b =>
                {
                    b.Navigation("Fines");
                });
#pragma warning restore 612, 618
        }
    }
}
