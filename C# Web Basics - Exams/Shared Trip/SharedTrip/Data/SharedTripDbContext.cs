using Microsoft.EntityFrameworkCore;

using SharedTrip.Data.Models;

namespace SharedTrip.Data
{
    public class SharedTripDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; init; }

        public DbSet<User> Users { get; init; }

        public DbSet<UserTrip> UserTrips { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>()
                        .HasKey(u => new { u.UserId, u.TripId });
        }
    }
}
