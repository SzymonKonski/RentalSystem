using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure.Configuration
{
    public class CarReservationEntityConfiguration : IEntityTypeConfiguration<CarReservation>
    {
        public void Configure(EntityTypeBuilder<CarReservation> modelBuilder)
        {
            modelBuilder
                .HasOne(s => s.Car)
                .WithMany(g => g.CarReservations)
                .HasForeignKey(s => s.CarId)
                .HasConstraintName("FK_Rentals_Cars");

            modelBuilder.Property(x => x.CarId).IsRequired();
            modelBuilder.Property(x => x.UserId).IsRequired();
            modelBuilder.Property(x => x.RentFrom).IsRequired();

            modelBuilder
                .Property(p => p.Payment)
                .HasColumnType("decimal(18,4)");
        }
    }
}
