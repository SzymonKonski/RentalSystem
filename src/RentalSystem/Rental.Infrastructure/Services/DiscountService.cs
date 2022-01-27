using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental.Infrastructure.Interfaces;

namespace Rental.Infrastructure.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly RentalCarDbContext context;

        public DiscountService(RentalCarDbContext context)
        {
            this.context = context;
        }

        public async Task<decimal> GetDiscountForNewUsers(string userId)
        {
            var carFromReservation = await context.Rentals.Where(x => x.UserId == userId).ToListAsync();

            return carFromReservation.Count <= 3 ? 10 : 0;
        }
    }
}
