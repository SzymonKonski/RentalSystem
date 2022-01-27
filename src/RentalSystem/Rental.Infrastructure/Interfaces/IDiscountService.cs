using System.Threading.Tasks;

namespace Rental.Infrastructure.Interfaces
{
    public interface IDiscountService
    {
        Task<decimal> GetDiscountForNewUsers(string userId);
    }
}
