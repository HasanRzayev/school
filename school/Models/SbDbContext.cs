using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace school.Models
{
    public partial class SbDbContext : DbContext
    {
        public SbDbContext()
        {
        }

        public SbDbContext(DbContextOptions<SbDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Holiday> Holidays { get; set; } = null!;
        public virtual DbSet<Parent> Parents { get; set; } = null!;
        public virtual DbSet<Ride> Rides { get; set; } = null!;
        public virtual DbSet<RideStudent> RideStudents { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=WIN-EA8010O87DM;Initial Catalog=SbDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasIndex(e => e.StudentId, "IX_Attendances_StudentId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentId);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasIndex(e => e.DriverId, "IX_Cars_DriverId")
                    .IsUnique()
                    .HasFilter("([DriverId] IS NOT NULL)");

                entity.HasOne(d => d.Driver)
                    .WithOne(p => p.Car)
                    .HasForeignKey<Car>(d => d.DriverId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.HasIndex(e => e.DriverId, "IX_Rides_DriverId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.DriverId);
            });

            modelBuilder.Entity<RideStudent>(entity =>
            {
                entity.HasIndex(e => e.RideId, "IX_RideStudents_RideId");

                entity.HasIndex(e => e.StudentId, "IX_RideStudents_StudentId");

                entity.HasOne(d => d.Ride)
                    .WithMany(p => p.RideStudents)
                    .HasForeignKey(d => d.RideId);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RideStudents)
                    .HasForeignKey(d => d.StudentId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.GroupId, "IX_Students_GroupId");

                entity.HasIndex(e => e.ParentId, "IX_Students_ParentId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ParentId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
