using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental.Infrastructure.Interfaces;
using RentalSystem.Domain.Entities;
using Sieve.Models;
using Sieve.Services;

namespace Rental.Infrastructure.Services
{
    public class CarService : ICarService
    {
        private readonly RentalCarDbContext context;
        private readonly SieveProcessor sieveProcessor;

        public CarService(RentalCarDbContext context, SieveProcessor sieveProcessor)
        {
            this.context = context;
            this.sieveProcessor = sieveProcessor;
        }

        public async Task<IEnumerable<Car>> GetCarsList(SieveModel carParameters)
        {
            if(carParameters == null)
                return await context.Cars.ToListAsync();

            var result = context.Cars.AsNoTracking();
            result = sieveProcessor.Apply(carParameters, result);
            result = result.Where(x => x.IsRented == false);

            return await result.ToListAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == id && x.IsRented == false);

            return car;
        }

        public async Task<Car> CreateCar(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
            return car;
        }

        public async Task UpdateCar(Car car)
        {
            context.Cars.Update(car);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCar(Car car)
        {
            context.Cars.Remove(car);
            await context.SaveChangesAsync();
        }
    }
}
