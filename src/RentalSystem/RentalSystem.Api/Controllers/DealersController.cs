using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Rental.Infrastructure.Interfaces;
using RentalSystem.Api.Dto.Dealers;
using RentalSystem.Domain.Entities;

namespace RentalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "AccessAsUser")]
    [ApiController]
    public class DealersController : ControllerBase
    {
        private readonly IDealerService dealerService;
        private readonly IMapper mapper;

        public DealersController(IDealerService dealerService, IMapper mapper)
        {
            this.dealerService = dealerService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DealerListDto>> Get()
        {
            var dealers = await dealerService.GetDealersList();
            IEnumerable<DealerListDto> dealersList = mapper.Map<IEnumerable<Dealer>, List<DealerListDto>>(dealers);

            return dealersList;
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<DealerDetailsDto>> Get(int id)
        {
            var dealer = await dealerService.GetDealerById(id);

            if (dealer == null)
            {
                return NotFound();
            }

            var dealerDetails =  mapper.Map<DealerDetailsDto>(dealer);
            return Ok(dealerDetails);
        }

        // POST: api/Dealers
        [HttpPost]
        public async Task<ActionResult<Dealer>> Post(CreateDealerDto createDealer)
        {
            var dealer = mapper.Map<Dealer>(createDealer);
            await dealerService.CreateDealer(dealer);
            return CreatedAtAction("Post", new { id = dealer.Id }, dealer);
        }

        // PUT: api/Dealers/5
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Put(int id, UpdateDealerDto updateDealer)
        {
            if (id != updateDealer.Id)
            {
                return BadRequest("Not a valid dealer id");
            }

            var dealer = await dealerService.GetDealerById(id);

            if (dealer == null)
                return NotFound();

            dealer.Name = updateDealer.Name;
            dealer.PhoneNumber = updateDealer.PhoneNumber;

            await dealerService.UpdateDealer(dealer);
            return NoContent();
        }

        // DELETE: api/Dealers/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid dealer id");

            var dealer = await dealerService.GetDealerById(id);
            if (dealer == null)
            {
                return NotFound();
            }

            await dealerService.DeleteDealer(dealer);

            return NoContent();
        }
    }
}
