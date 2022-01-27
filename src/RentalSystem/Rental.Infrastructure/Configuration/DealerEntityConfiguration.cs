using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure.Configuration
{
    public class DealerEntityConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).IsRequired();
        }
    }
}
