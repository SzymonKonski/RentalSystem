using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Rental.Infrastructure.Interfaces;
using RentalSystem.Api.Dto;
using RentalSystem.Api.Dto.Reservations;

namespace RentalSystem.Api.Controllers
{
    [Route("api/reservations")]
    [Authorize(Policy = "AccessAsUser")]
    [ApiController]
    public class UserReservationsController : ControllerBase
    {
        private readonly IUserCarReservationService userCarReservationService;
        private readonly IMapper mapper;
        private readonly IUploadService uploadService;
        private readonly ICarReservationMessagingService carReservationMessagingService;


        public UserReservationsController(IUserCarReservationService userCarReservationService, IMapper mapper, IUploadService uploadService, ICarReservationMessagingService carReservationMessagingService)
        {
            this.userCarReservationService = userCarReservationService;
            this.mapper = mapper;
            this.uploadService = uploadService;
            this.carReservationMessagingService = carReservationMessagingService;
        }

        /// <summary>
        /// Make reservation for a specific car
        /// </summary>
        /// <response code="201">Created customer reservation for a specific car</response>
        /// <response code="401">Access denied</response>
        /// <response code="400">Model is not valid or car is already reserved</response>
        /// <response code="500">Oops! something went wrong</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarReservationDto customerCarReservation)
        {
            var carReservation = new Domain.Entities.CarReservation
            {
                CarId = customerCarReservation.CarId,
                ImageUrl = "",
                PdfUrl = "",
                RentFrom = customerCarReservation.RentFrom,
                RentTo = customerCarReservation.RentTo
            };

            var operationResult = await userCarReservationService.MakeReservationAsync(carReservation);

            if (operationResult.CompletedWithSuccess)
            {
                await carReservationMessagingService.PublishNewCarReservationMessageAsync(operationResult.Result);
                return StatusCode(StatusCodes.Status201Created);
            }

            else
            {
                return BadRequest(operationResult.OperationError);
            }
        }

        [HttpGet("GetCurrent")]
        public async Task<IEnumerable<UserReservationDto>> GetCurrent()
        {
            var rentals = await userCarReservationService.GetCurrentReservations();
            IEnumerable<UserReservationDto> reservationList = mapper.Map<IEnumerable<Domain.Entities.CarReservation>, List<UserReservationDto>>(rentals);

            return reservationList;
        }

        [HttpGet("GetOld")]
        public async Task<IEnumerable<UserReservationDto>> GetOld()
        {
            var rentals = await userCarReservationService.GetOldReservations();
            IEnumerable<UserReservationDto> reservationList = mapper.Map<IEnumerable<Domain.Entities.CarReservation>, List<UserReservationDto>>(rentals);

            return reservationList;
        }

        [HttpGet("Payment/{id}")]
        public async Task<decimal> GetPayment(int id)
        {
            var payment = await userCarReservationService.GetPayment(id);
            return payment;
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
                    else if (file.ContentType == "image/png" || file.ContentType == "image/jpeg")
                    {
                        imageUrl = await uploadService.UploadImageAsync(file.OpenReadStream(), file.ContentType);
                    }
                }

                await userCarReservationService.ReturnReservation(id, pdfUrl, imageUrl);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
