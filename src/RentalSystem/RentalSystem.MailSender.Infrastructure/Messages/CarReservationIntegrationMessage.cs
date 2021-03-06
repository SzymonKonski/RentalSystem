using System;
using System.Text.Json.Serialization;

namespace RentalSystem.MailSender.Infrastructure.Messages
{
    public class CarReservationIntegrationMessage : IntegrationMessage
    {
        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }
        [JsonPropertyName("carId")]
        public int CarId { get; set; }
        [JsonPropertyName("rentFrom")]
        public DateTime RentFrom { get; set; }

        [JsonPropertyName("rentTo")]
        public DateTime RentTo { get; set; }
    }
}
