namespace RentalSystem.MailSender.Core.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsRented { get; set; }
        public int DealerId { get; set; }
    }
}
