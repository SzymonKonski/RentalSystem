using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure.Configuration
{
    public class CarEntityConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> modelBuilder)
        {
            modelBuilder.HasIndex(c => c.Model);
            modelBuilder.HasIndex(c => c.Horsepower);
            modelBuilder.HasIndex(c => c.Brand);

            modelBuilder
                .Property(t => t.Description)
                .HasMaxLength(500);

            modelBuilder
                .Property(t => t.Brand)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder
                .Property(p => p.Model)
                .IsRequired();

            modelBuilder
                .Property(p => p.Horsepower)
                .IsRequired();

            modelBuilder
                .Property(p => p.Brand)
                .IsRequired();

            modelBuilder
                .Property(p => p.BasePrice)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            modelBuilder
                .Property(p => p.IsRented)
                .IsRequired();

            modelBuilder
                .Property(p => p.YearOfProduction)
                .IsRequired();

            modelBuilder
                .HasOne(c => c.Dealer)
                .WithMany(d => d.Cars)
                .HasForeignKey(c => c.DealerId)
                .HasConstraintName("FK_Cars_Dealers");
        }
    }
}
