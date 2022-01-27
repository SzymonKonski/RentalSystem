using System.Collections.Generic;
using System.Threading.Tasks;
using RentalSystem.Domain.Entities;
using Sieve.Models;

namespace Rental.Infrastructure.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCarsList(SieveModel carParameters = null);
        Task<Car> GetCarById(int id);
        Task<Car> CreateCar(Car car);
        Task UpdateCar(Car car);
        Task DeleteCar(Car car);
    }
}
