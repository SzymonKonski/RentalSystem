namespace RentalSystem.Api.Dto.Cars
{
    public class CreateCarDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Horsepower { get; set; }
        public int YearOfProduction { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int DealerId { get; set; }
    }
}
