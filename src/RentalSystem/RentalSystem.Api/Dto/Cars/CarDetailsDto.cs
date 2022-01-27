namespace RentalSystem.Api.Dto.Cars
{
    public class CarDetailsDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Horsepower { get; set; }
        public int YearOfProduction { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string DealerName { get; set; }
        public int DealerId { get; set; }
    }
}
