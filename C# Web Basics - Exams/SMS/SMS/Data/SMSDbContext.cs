using Microsoft.EntityFrameworkCore;

using SMS.Data.Models;
using System;

namespace SMS.Data
{
    public class SMSDbContext : DbContext
    {
        public SMSDbContext()
        {
        }

        public DbSet<Cart> Carts { get; init; }

        public DbSet<Product> Products { get; init; }

        public DbSet<User> Users { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        internal object Where()
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasOne<User>(u => u.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<User>(k => k.Id);
        }
    }
}