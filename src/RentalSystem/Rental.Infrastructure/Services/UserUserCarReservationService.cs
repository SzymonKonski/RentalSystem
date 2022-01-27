using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental.Infrastructure.Common;
using Rental.Infrastructure.Interfaces;

namespace Rental.Infrastructure.Services
{
    public class UserUserCarReservationService : IUserCarReservationService
    {
        private readonly RentalCarDbContext context;
        private readonly IIdentityService identityService;
        private readonly IDiscountService discountService;

        public UserUserCarReservationService(RentalCarDbContext context, IIdentityService identityService, IDiscountService discountService)
        {
            this.context = context;
            this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            this.discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        }

        public async Task<OperationResponse<RentalSystem.Domain.Entities.CarReservation>> MakeReservationAsync(RentalSystem.Domain.Entities.CarReservation carReservation)
        {
            var carFromReservation = await context.Cars.FirstOrDefaultAsync(x => x.Id == carReservation.CarId);
            if (carFromReservation == null)
            {
                return new OperationResponse<RentalSystem.Domain.Entities.CarReservation>()
                    .SetAsFailureResponse(OperationErrorDictionary.CarReservation.CarDoesNotExist());
            }

            if (carFromReservation.IsRented)
            {
                return new OperationResponse<RentalSystem.Domain.Entities.CarReservation>()
                    .SetAsFailureResponse(OperationErrorDictionary.CarReservation.CarAlreadyReserved());
            }
            else
            {
                var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == carReservation.CarId);
                car.IsRented = true;
                carReservation.UserId = identityService.GetUserIdentity().ToString();
                await context.Rentals.AddAsync(carReservation);
                context.Cars.Update(car);
                await context.SaveChangesAsync();

                return new OperationResponse<RentalSystem.Domain.Entities.CarReservation>(carReservation);
            }
        }

        public async Task ReturnReservation(int id, string pdfUrl, string imageUrl)
        {
            var rental = await context.Rentals.FirstOrDefaultAsync(x => x.Id == id);
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
        }

        public async Task<decimal> GetPayment(int rentalId)
        {
            var rental = await context.Rentals.FirstOrDefaultAsync(x => x.Id == rentalId);
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == rental.CarId);

            var discount = await discountService.GetDiscountForNewUsers(rental.UserId);
            var payment = ((int)(DateTime.Now - rental.RentFrom).TotalDays + 1) * (car.BasePrice - discount);

            return payment;
        }

        public async Task<IEnumerable<RentalSystem.Domain.Entities.CarReservation>> GetCurrentReservations()
        {
            var userId = identityService.GetUserIdentity().ToString();

            var rentals = await context.Rentals.Where(x => x.UserId == userId && x.Returned == false)
                .Include(x => x.Car).ToListAsync();

            return rentals;
        }

        public async Task<IEnumerable<RentalSystem.Domain.Entities.CarReservation>> GetOldReservations()
        {
            var userId = identityService.GetUserIdentity().ToString();

            var rentals = await context.Rentals.Where(x => x.UserId == userId && x.Returned == true)
                .Include(x => x.Car).ToListAsync();

            return rentals;
        }
    }
}
