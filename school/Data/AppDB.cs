using Microsoft.EntityFrameworkCore;
using Sb.Models.Entities;

namespace Sb.Data
{
    public class AppDB : DbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Holiday> Holidays { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=STHQ0125-06;Initial Catalog=SbDb;User ID=admin;Password=admin;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Car>()
            //    .HasAlternateKey(e => e.Number);
            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Driver)
                .WithOne(d => d.Ride)
                .HasForeignKey<Ride>(r => r.DriverId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Driver)
                .WithOne(d => d.Car)
                .HasForeignKey<Car>(c => c.DriverId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Ride)
                .WithMany(r => r.Students)
                .HasForeignKey(s => s.RideId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
