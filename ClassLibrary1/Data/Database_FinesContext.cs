using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClassLibrary1.Data
{
    public partial class Database_FinesContext : DbContext
    {
        public Database_FinesContext()
        {
        }

        public Database_FinesContext(DbContextOptions<Database_FinesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Fine> Fines { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonCar> PersonCars { get; set; }
        public virtual DbSet<Registrator> Registrators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=NB-SW41\\SQLEXPRESS;Database=Database_Fines;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Fine>(entity =>
            {
                entity.ToTable("Fine");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DateTmeOfAccident).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("money");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Fines)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK_Fine_Car");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Fines)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Fine_Person");

                entity.HasOne(d => d.Registrator)
                    .WithMany(p => p.Fines)
                    .HasForeignKey(d => d.RegistratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fine_Registrator");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PersonCar>(entity =>
            {
                entity.HasKey(e => new { e.CarId, e.PersonId });

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.PersonCars)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonCars_Car");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonCars)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonCars_Person");
            });

            modelBuilder.Entity<Registrator>(entity =>
            {
                entity.ToTable("Registrator");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.GerNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Registrators)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Registrator_Person");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
