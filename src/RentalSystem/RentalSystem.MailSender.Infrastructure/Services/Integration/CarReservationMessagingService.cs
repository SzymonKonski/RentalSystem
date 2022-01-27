using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentalSystem.MailSender.Infrastructure.Messages;
using RentalSystem.MailSender.Infrastructure.Services.Integration.Interfaces;
using RentalSystem.MailSender.Infrastructure.Services.Mail.Interfaces;
using RentalSystem.MailSender.Infrastructure.Services.Mail.Templates;
using RentalSystem.MailSender.Infrastructure.Services.MsGraph.Interfaces;

namespace RentalSystem.MailSender.Infrastructure.Services.Integration
{
    public class CarReservationMessagingService : ICarReservationMessagingService
    {
        private readonly IMsGraphSdkClientService msGraphSdkClientService;
        private readonly IMailDeliveryService mailDeliveryService;
        private readonly RentalCarDbContext context;
        private readonly ILogger<CarReservationMessagingService> logger;

        public CarReservationMessagingService(IMsGraphSdkClientService msGraphSdkClientService,
                                              IMailDeliveryService mailDeliveryService,
                                              RentalCarDbContext context,
                                              ILogger<CarReservationMessagingService> logger)
        {
            this.msGraphSdkClientService = msGraphSdkClientService
                                           ?? throw new ArgumentNullException(nameof(msGraphSdkClientService));

            this.mailDeliveryService = mailDeliveryService
                                       ?? throw new ArgumentNullException(nameof(mailDeliveryService));

            this.context = context;

            this.logger = logger
                          ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleNewCarReservationMessageAsync(string carReservationIntegrationMessageAsString)
        {
            var carReservationIntegrationMessage = JsonSerializer.Deserialize<CarReservationIntegrationMessage>(carReservationIntegrationMessageAsString);
            if (carReservationIntegrationMessage != null && !string.IsNullOrEmpty(carReservationIntegrationMessage.CustomerId))
            {
                var customer = await msGraphSdkClientService.GetUserAsync(carReservationIntegrationMessage.CustomerId);

                if (customer != null)
                {
                    var carFromReservation = await context.Cars.FirstOrDefaultAsync(x => x.Id == carReservationIntegrationMessage.CarId);

                    if (carFromReservation != null)
                    {
                        var carReservationConfirmationMailTemplate = new CarReservationConfirmationMailTemplate
                        {
                            CustomerName = $"{customer.FirstName} {customer.LastName}",
                            CustomerEmail = customer.Email,
                            CarBrand = carFromReservation.Brand,
                            CarModel = carFromReservation.Model,
                            FromDate = carReservationIntegrationMessage.RentFrom.ToString("dd/MM/yyyy"),
                            ToDate = carReservationIntegrationMessage.RentTo.ToString("dd/MM/yyyy")
                        };

                        await mailDeliveryService.SendInvitationMessageAsync(carReservationConfirmationMailTemplate);
                    }
                }
            }

            else
            {
                logger.LogError($"Customer ID value in the queue message cannot be empty. Row content: {carReservationIntegrationMessageAsString}");
            }
        }
    }
}
