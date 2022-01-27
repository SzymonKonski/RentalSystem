using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using RentalSystem.MailSender.Infrastructure.Services.Integration.Interfaces;

namespace RentalSystem.MailSender.FuncApp
{
    public class MailSenderFuncApp
    {
        private readonly ICarReservationMessagingService carReservationMessagingService;

        public MailSenderFuncApp(ICarReservationMessagingService carReservationMessagingService)
        {
            this.carReservationMessagingService = carReservationMessagingService
                                                  ?? throw new ArgumentNullException(nameof(carReservationMessagingService));
        }

        [FunctionName("mail-sender-func-app")]
        public async Task RunAsync([ServiceBusTrigger("reservations", Connection = "AzureServiceBusConnectionString")] string queueItem)
        {
            await carReservationMessagingService.HandleNewCarReservationMessageAsync(queueItem);
        }
    }
}
