using System.Threading.Tasks;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure.Interfaces
{
    public interface ICarReservationMessagingService
    {
        Task PublishNewCarReservationMessageAsync(CarReservation carReservation);
    }
}
