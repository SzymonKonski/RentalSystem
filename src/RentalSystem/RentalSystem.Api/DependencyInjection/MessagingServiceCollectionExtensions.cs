using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rental.Infrastructure.Configuration.Interfaces;
using Rental.Infrastructure.Interfaces;
using Rental.Infrastructure.Services;

namespace RentalSystem.Api.DependencyInjection
{
    public static class MessagingServiceCollectionExtensions
    {
        public static IServiceCollection AddMessagingServices(this IServiceCollection services)
        {
            services.TryAddSingleton(implementationFactory =>
            {
                var serviceBusConfiguration = implementationFactory.GetRequiredService<IMessagingServiceConfiguration>();
                var serviceBusClient = new ServiceBusClient(serviceBusConfiguration.ListenAndSendConnectionString);
                var serviceBusSender = serviceBusClient.CreateSender(serviceBusConfiguration.QueueName);
                return serviceBusSender;
            });

            services.AddSingleton<ICarReservationMessagingService, CarReservationMessagingService>();

            return services;
        }
    }
}
