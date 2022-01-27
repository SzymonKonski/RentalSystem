using System.Threading.Tasks;

namespace RentalSystem.MailSender.Infrastructure.Services.Integration.Interfaces
{
    public interface ICarReservationMessagingService
    {
        Task HandleNewCarReservationMessageAsync(string carReservationIntegrationMessageAsString);
    }
}
