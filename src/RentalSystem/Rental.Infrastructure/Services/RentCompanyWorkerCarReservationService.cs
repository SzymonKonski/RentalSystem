using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental.Infrastructure.Interfaces;

namespace Rental.Infrastructure.Services
{
    public class RentCompanyWorkerCarReservationService : IRentCompanyWorkerCarReservationService
    {
        private readonly RentalCarDbContext context;
        private readonly IDiscountService discountService;

        public RentCompanyWorkerCarReservationService(RentalCarDbContext context, IDiscountService discountService)
        {
            this.context = context;
            this.discountService = discountService;
        }

        public async Task<IEnumerable<RentalSystem.Domain.Entities.CarReservation>> GetCurrentReservations()
        {
            var rentals = await context.Rentals.Where(x => x.Returned == false)
                .Include(x => x.Car).ToListAsync();

            return rentals;
        }

        public async Task<IEnumerable<RentalSystem.Domain.Entities.CarReservation>> GetOldReservations()
        {
            var rentals = await context.Rentals.Where(x => x.Returned == true)
                .Include(x => x.Car).ToListAsync();

            return rentals;
        }

        public async Task<decimal> ReturnReservation(int rentId, string pdfUrl, string imageUrl)
        {
            var rental = await context.Rentals.FirstOrDefaultAsync(x => x.Id == rentId);
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == rental.CarId);
            car.IsRented = false;
            rental.ImageUrl = imageUrl;
            rental.PdfUrl = pdfUrl;
            rental.RentTo = DateTime.Now;
            rental.Returned = true;

            var discount = await discountService.GetDiscountForNewUsers(rental.UserId);
            var payment = ((int)(DateTime.Now - rental.RentFrom).TotalDays + 1) * (car.BasePrice - discount);
            rental.Payment = payment;

            context.Rentals.Update(rental);
            context.Cars.Update(car);
            await context.SaveChangesAsync();

            return payment;
        }

        public async Task<decimal> GetPayment(int rentalId)
        {
            var rental = await context.Rentals.FirstOrDefaultAsync(x => x.Id == rentalId);
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == rental.CarId);

            var discount = await discountService.GetDiscountForNewUsers(rental.UserId);
            var payment = ((int)(DateTime.Now - rental.RentFrom).TotalDays + 1) * (car.BasePrice - discount);

            return payment;
        }
    }
}
