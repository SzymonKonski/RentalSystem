using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RentalSystem.MailSender.Infrastructure.Configuration.Interfaces;
using RentalSystem.MailSender.Infrastructure.Services.Mail.Interfaces;
using RentalSystem.MailSender.Infrastructure.Services.Mail.Templates;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RentalSystem.MailSender.Infrastructure.Services.Mail
{
    public class MailDeliveryService : IMailDeliveryService
    {
        private readonly ISendGridClient sendGridClient;
        private readonly IMailDeliveryServiceConfiguration mailDeliveryServiceConfiguration;
        private readonly ILogger<MailDeliveryService> logger;

        public MailDeliveryService(ISendGridClient sendGridClient,
                                   IMailDeliveryServiceConfiguration mailDeliveryServiceConfiguration,
                                    ILogger<MailDeliveryService> logger)
        {
            this.sendGridClient = sendGridClient
                                  ?? throw new ArgumentNullException(nameof(sendGridClient));

            this.mailDeliveryServiceConfiguration = mailDeliveryServiceConfiguration
                                                    ?? throw new ArgumentNullException(nameof(mailDeliveryServiceConfiguration));

            this.logger = logger
                          ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendInvitationMessageAsync(CarReservationConfirmationMailTemplate carReservationConfirmationMailTemplate)
        {
            var emailMessage = MailHelper.CreateSingleTemplateEmail(
                 new EmailAddress(mailDeliveryServiceConfiguration.FromEmail),
                 new EmailAddress(carReservationConfirmationMailTemplate.CustomerEmail),
                 mailDeliveryServiceConfiguration.CarReservationConfirmationTemplateId,
                 new
                 {
                     customerName = carReservationConfirmationMailTemplate.CustomerName,
                     carBrand = carReservationConfirmationMailTemplate.CarBrand,
                     carModel = carReservationConfirmationMailTemplate.CarModel,
                     fromDate = carReservationConfirmationMailTemplate.FromDate,
                     toDate = carReservationConfirmationMailTemplate.ToDate
                 });

            var response = await sendGridClient.SendEmailAsync(emailMessage);
            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                var responseContent = await response.Body.ReadAsStringAsync();
                logger.LogError($"SendGrid service returned status code {response.StatusCode} with response: {responseContent}");
            }
        }
    }
}
