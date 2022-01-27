using System.Collections.Generic;
using Sieve.Attributes;

namespace RentalSystem.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Brand { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Model { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int Horsepower { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int YearOfProduction { get; set; }
        public string Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public decimal BasePrice { get; set; }
        public bool IsRented { get; set; }
        public ICollection<CarReservation> CarReservations { get; set; }
        public int DealerId { get; set; }
        public Dealer Dealer { get; set; }
    }
}
