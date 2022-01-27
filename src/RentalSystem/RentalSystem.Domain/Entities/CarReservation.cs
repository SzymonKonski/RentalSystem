using System;

namespace RentalSystem.Domain.Entities
{
    public class CarReservation
    {
        public int Id { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime? RentTo { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        public string PdfUrl { get; set; }
        public decimal? Payment { get; set; }
        public bool Returned { get; set; }
    }
}
