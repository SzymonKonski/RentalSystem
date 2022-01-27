using System.Collections.Generic;
using System.Threading.Tasks;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure.Interfaces
{
    public interface IRentCompanyWorkerCarReservationService
    {
        Task<IEnumerable<CarReservation>> GetCurrentReservations();

        Task<IEnumerable<CarReservation>> GetOldReservations();

        Task<decimal> ReturnReservation(int id, string pdfUrl, string imageUrl);

        Task<decimal> GetPayment(int rentalId);
    }
}
