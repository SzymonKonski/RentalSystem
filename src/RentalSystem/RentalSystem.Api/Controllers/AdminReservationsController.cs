using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.Infrastructure.Interfaces;
using RentalSystem.Api.Dto.Reservations;

namespace RentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/reservations/admin")]
    [Authorize(Policy = "AccessAsUser")]
    [Authorize(Policy = "Admin")]
    public class AdminReservationsController : ControllerBase
    {
        private readonly IRentCompanyWorkerCarReservationService rentCompanyWorkerCarReservationService;
        private readonly IMapper mapper;
        private readonly IUploadService uploadService;

        public AdminReservationsController(IRentCompanyWorkerCarReservationService rentCompanyWorkerCarReservationService, IMapper mapper, IUploadService uploadService)
        {
            this.rentCompanyWorkerCarReservationService = rentCompanyWorkerCarReservationService;
            this.mapper = mapper;
            this.uploadService = uploadService;
        }

        [HttpGet("Payment/{id}")]
        public async Task<decimal> GetPayment(int id)
        {
            var payment = await rentCompanyWorkerCarReservationService.GetPayment(id);
            return payment;
        }

        [HttpGet("GetCurrent")]
        public async Task<IEnumerable<UserReservationDto>> GetCurrent()
        {
            var rentals = await rentCompanyWorkerCarReservationService.GetCurrentReservations();
            IEnumerable<UserReservationDto> reservationList = mapper.Map<IEnumerable<Domain.Entities.CarReservation>, List<UserReservationDto>>(rentals);

            return reservationList;
        }

        [HttpGet("GetOld")]
        public async Task<IEnumerable<UserReservationDto>> GetOld()
        {
            var rentals = await rentCompanyWorkerCarReservationService.GetOldReservations();
            IEnumerable<UserReservationDto> reservationList = mapper.Map<IEnumerable<Domain.Entities.CarReservation>, List<UserReservationDto>>(rentals);

            return reservationList;
        }

        [HttpPost("ReturnMe/{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> ReturnMe(int id)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();

                if (formCollection.Files.Any(f => f.Length < 0))
                    return BadRequest();

                string pdfUrl = null;
                string imageUrl = null;

                foreach (var file in formCollection.Files)
                {
                    if (file.ContentType == "application/pdf")
                    {
                        pdfUrl = await uploadService.UploadPdfAsync(file.OpenReadStream(), file.ContentType);
                    }
                    else if (file.ContentType is "image/png" or "image/jpeg")
                    {
                        imageUrl = await uploadService.UploadImageAsync(file.OpenReadStream(), file.ContentType);
                    }
                }

                var payment = await rentCompanyWorkerCarReservationService.ReturnReservation(id, pdfUrl, imageUrl);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
