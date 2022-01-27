using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental.Infrastructure.Interfaces;
using RentalSystem.Domain.Entities;

namespace Rental.Infrastructure.Services
{
    public class DealerService : IDealerService
    {
        private readonly RentalCarDbContext context;

        public DealerService(RentalCarDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Dealer>> GetDealersList()
        {
            var result = await context.Dealers.ToListAsync();
            return result;
        }

        public async Task<Dealer> GetDealerById(int id)
        {
            var dealer = await context.Dealers.FirstOrDefaultAsync(x => x.Id == id);
            return dealer;
        }

        public async Task<Dealer> CreateDealer(Dealer dealer)
        {
            context.Dealers.Add(dealer);
            await context.SaveChangesAsync();
            return dealer;
        }

        public async Task UpdateDealer(Dealer dealer)
        {
            context.Dealers.Update(dealer);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDealer(Dealer dealer)
        {
            context.Dealers.Remove(dealer);
            await context.SaveChangesAsync();
        }
    }
}
