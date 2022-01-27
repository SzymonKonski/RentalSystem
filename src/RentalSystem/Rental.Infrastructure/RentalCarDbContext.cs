using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure
{
    public class RentalCarDbContext : DbContext
    {
        public RentalCarDbContext(DbContextOptions<RentalCarDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarReservation> Rentals { get; set; }
        public DbSet<Dealer> Dealers { get; init; }
        public DbSet<UserAuthorizationGroups> UserAuthorizationGroups { get; set; }
        public DbSet<AuthorizationGroups> AuthorizationGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}