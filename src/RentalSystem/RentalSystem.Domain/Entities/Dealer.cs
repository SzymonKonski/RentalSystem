using System.Collections.Generic;

namespace RentalSystem.Domain.Entities
{
    public class Dealer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
