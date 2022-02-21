namespace FootballManager.Data
{
    using FootballManager.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class FootballManagerDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserPlayer> UserPlayers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=FootballManager;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPlayer>(entity =>
            {
                entity.HasKey(up => new { up.UserId, up.PlayerId });
                entity
                .HasOne(up => up.User)
                .WithMany(u => u.UserPlayers)
                .HasForeignKey(u => u.UserId);

                entity
                .HasOne(up => up.Player)
                .WithMany(p => p.UserPlayers)
                .HasForeignKey(up => up.PlayerId);
            });
        }
    }
}
//modelBuilder.Entity<UserTrip>(entity =>
//{
//    entity.HasKey(sc => new { sc.TripId, sc.UserId });
//    entity
//    .HasOne(sc => sc.User)
//    .WithMany(s => s.UserTrips)
//    .HasForeignKey(sc => sc.UserId);
//    entity
//    .HasOne(sc => sc.Trip)
//    .WithMany(c => c.UserTrips)
//    .HasForeignKey(sc => sc.TripId);
//});