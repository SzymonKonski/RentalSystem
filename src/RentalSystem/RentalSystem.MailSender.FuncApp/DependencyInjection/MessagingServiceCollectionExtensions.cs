using Microsoft.Extensions.DependencyInjection;
using RentalSystem.MailSender.Infrastructure.Services.Integration;
using RentalSystem.MailSender.Infrastructure.Services.Integration.Interfaces;

namespace RentalSystem.MailSender.FuncApp.DependencyInjection
{
    internal static class MessagingServiceCollectionExtensions
    {
        public static IServiceCollection AddCarReservationMessagingService(this IServiceCollection services)
        {
            services.AddScoped<ICarReservationMessagingService, CarReservationMessagingService>();
            return services;
        }
    }
}
