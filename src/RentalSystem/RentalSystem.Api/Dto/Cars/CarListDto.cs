namespace RentalSystem.Api.Dto.Cars
{
    public class CarListDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public decimal BasePrice { get; set; }
    }
}
