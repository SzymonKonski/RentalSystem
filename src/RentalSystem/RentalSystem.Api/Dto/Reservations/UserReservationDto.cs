using System;
using RentalSystem.Domain.Entities;

namespace RentalSystem.Api.Dto.Reservations
{
    public class UserReservationDto
    {
        public int Id { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime? RentTo { get; set; }
        public Car Car { get; set; }
    }
}
