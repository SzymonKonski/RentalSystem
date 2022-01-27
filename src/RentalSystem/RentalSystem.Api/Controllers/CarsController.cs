using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Rental.Infrastructure.Interfaces;
using RentalSystem.Api.Dto.Cars;
using RentalSystem.Domain.Entities;
using Sieve.Models;

namespace RentalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "AccessAsUser")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IMapper mapper;

        public CarsController(ICarService carService, IMapper mapper, IDealerService dealerService)
        {
            this.carService = carService;
            this.mapper = mapper;
            this.dealerService = dealerService;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<IEnumerable<CarListDto>> Get([FromQuery] SieveModel sieveModel)
        {
            var cars = await carService.GetCarsList(sieveModel);
            IEnumerable<CarListDto> carsList = mapper.Map<IEnumerable<Car>, List<CarListDto>>(cars);

            return carsList;
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDetailsDto>> Get(int id)
        {
            var car = await carService.GetCarById(id);

            if (car == null)
                return NotFound();

            var dealer = await dealerService.GetDealerById(car.DealerId);

            if (dealer == null) 
                return NotFound();

            var carDetails = new CarDetailsDto
            {
                Id = car.Id,
                BasePrice = car.BasePrice,
                Brand = car.Brand,
                Horsepower = car.Horsepower,
                Model = car.Model,
                YearOfProduction = car.YearOfProduction,
                Description = car.Description,
                DealerName = dealer.Name,
                DealerId = dealer.Id
            };

            return Ok(carDetails);
        }

        // POST: api/Cars
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Car>> Post(CreateCarDto createCar)
        {
            var car = mapper.Map<Car>(createCar);
            await carService.CreateCar(car);
            return CreatedAtAction("Post", new { id = car.Id }, car);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Put(int id, UpdateCarDto updatedCar)
        {
            if (id != updatedCar.Id)
            {
                return BadRequest("Not a valid car id");
            }

            var car = await carService.GetCarById(id);

            if (car == null)
                return NotFound();

            car.Brand = updatedCar.Brand;
            car.Description = updatedCar.Description;
            car.Horsepower = updatedCar.Horsepower;
            car.YearOfProduction = updatedCar.YearOfProduction;
            car.Model = updatedCar.Model;

            await carService.UpdateCar(car);
            return NoContent();
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid car id");

            var car = await carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            await carService.DeleteCar(car);

            return NoContent();
        }
    }
}
