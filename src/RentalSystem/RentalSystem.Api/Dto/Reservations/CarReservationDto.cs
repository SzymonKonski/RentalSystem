using System;

namespace RentalSystem.Api.Dto.Reservations
{
    public class CarReservationDto
    {
        public int CarId { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }
    }
}
