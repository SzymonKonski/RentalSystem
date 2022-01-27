using Microsoft.Extensions.DependencyInjection;
using RentalSystem.MailSender.Infrastructure.Configuration.Interfaces;
using RentalSystem.MailSender.Infrastructure.Services.Mail;
using RentalSystem.MailSender.Infrastructure.Services.Mail.Interfaces;
using SendGrid.Extensions.DependencyInjection;

namespace RentalSystem.MailSender.FuncApp.DependencyInjection
{
    internal static class MailDeliveryServiceCollectionExtensions
    {
        public static IServiceCollection AddMailDeliveryServices(this IServiceCollection services)
        {
            services.AddSendGrid((sp, options) =>
            {
                var mailDeliveryServiceConfiguration = sp.GetRequiredService<IMailDeliveryServiceConfiguration>();
                options.ApiKey = mailDeliveryServiceConfiguration.ApiKey;
            });

            services.AddScoped<IMailDeliveryService, MailDeliveryService>();
            return services;
        }
    }
}
