using System.Collections.Generic;
using System.Threading.Tasks;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure.Interfaces
{
    public interface IDealerService
    {
        Task<IEnumerable<Dealer>> GetDealersList();
        Task<Dealer> GetDealerById(int id);
        Task<Dealer> CreateDealer(Dealer dealer);
        Task UpdateDealer(Dealer dealer);
        Task DeleteDealer(Dealer dealer);
    }
}
