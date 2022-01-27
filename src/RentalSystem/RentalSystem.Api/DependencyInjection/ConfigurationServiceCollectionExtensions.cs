using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rental.Infrastructure.Configuration;
using Rental.Infrastructure.Configuration.Interfaces;

namespace RentalSystem.Api.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MessagingServiceConfiguration>(config.GetSection("ServiceBusSettings"));
            services.AddSingleton<IValidateOptions<MessagingServiceConfiguration>, MessagingServiceConfigurationValidation>();
            var messagingServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<MessagingServiceConfiguration>>().Value;
            services.AddSingleton<IMessagingServiceConfiguration>(messagingServiceConfiguration);

            return services;
        }
    }
}
