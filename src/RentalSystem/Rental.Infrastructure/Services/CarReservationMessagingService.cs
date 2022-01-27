using Azure.Messaging.ServiceBus;
using Rental.Infrastructure.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Rental.Infrastructure.Messages;

namespace Rental.Infrastructure.Services
{
    public class CarReservationMessagingService : ICarReservationMessagingService
    {
        private readonly ServiceBusSender serviceBusSender;

        public CarReservationMessagingService(ServiceBusSender serviceBusSender)
        {
            this.serviceBusSender = serviceBusSender
                                    ?? throw new ArgumentNullException(nameof(serviceBusSender));
        }

        public async Task PublishNewCarReservationMessageAsync(RentalSystem.Domain.Entities.CarReservation carReservation)
        {
            if (carReservation.RentTo != null)
            {
                var carReservationIntegrationMessage = new CarReservationIntegrationMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    CarId = carReservation.CarId,
                    CustomerId = carReservation.UserId,
                    RentFrom = carReservation.RentFrom,
                    RentTo = (DateTime)carReservation.RentTo
                };

                var serializedMessage = JsonSerializer.Serialize(carReservationIntegrationMessage);
                var message = new ServiceBusMessage(serializedMessage);
                await serviceBusSender.SendMessageAsync(message);
            }
        }
    }
}
