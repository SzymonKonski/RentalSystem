using System.Collections.Generic;
using System.Threading.Tasks;
using Rental.Infrastructure.Common;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure.Interfaces
{
    public interface IUserCarReservationService
    {
        Task<OperationResponse<CarReservation>> MakeReservationAsync(CarReservation carReservation);

        Task<IEnumerable<CarReservation>> GetCurrentReservations();

        Task<IEnumerable<CarReservation>> GetOldReservations();

        Task ReturnReservation(int id, string pdfUrl, string imageUrl);

        Task<decimal> GetPayment(int rentalId);
    }
}
