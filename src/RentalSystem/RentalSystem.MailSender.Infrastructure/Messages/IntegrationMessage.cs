using System.Text.Json.Serialization;

namespace RentalSystem.MailSender.Infrastructure.Messages
{
    public abstract class IntegrationMessage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
