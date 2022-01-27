using Microsoft.EntityFrameworkCore;
using RentalSystem.MailSender.Core.Entities;

namespace RentalSystem.MailSender.Infrastructure
{
    public class RentalCarDbContext : DbContext
    {
        public RentalCarDbContext(DbContextOptions<RentalCarDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}