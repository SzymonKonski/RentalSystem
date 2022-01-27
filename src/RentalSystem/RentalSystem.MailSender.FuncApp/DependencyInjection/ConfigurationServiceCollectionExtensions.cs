using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RentalSystem.MailSender.Infrastructure;
using RentalSystem.MailSender.Infrastructure.Configuration;
using RentalSystem.MailSender.Infrastructure.Configuration.Interfaces;
using MsGraphServiceConfiguration = RentalSystem.MailSender.Infrastructure.Configuration.MsGraphServiceConfiguration;
using MsGraphServiceConfigurationValidation = RentalSystem.MailSender.Infrastructure.Configuration.MsGraphServiceConfigurationValidation;

namespace RentalSystem.MailSender.FuncApp.DependencyInjection
{
    internal static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MsGraphServiceConfiguration>(config.GetSection("MsGraphServiceConfiguration"));
            services.AddSingleton<IValidateOptions<MsGraphServiceConfiguration>, MsGraphServiceConfigurationValidation>();
            var msGraphServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<MsGraphServiceConfiguration>>().Value;
            services.AddSingleton<IMsGraphServiceConfiguration>(msGraphServiceConfiguration);

            services.Configure<MailDeliveryServiceConfiguration>(config.GetSection("MailDeliveryServiceConfiguration"));
            services.AddSingleton<IValidateOptions<MailDeliveryServiceConfiguration>, MailDeliveryServiceConfigurationValidation>();
            var mailDeliveryServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<MailDeliveryServiceConfiguration>>().Value;
            services.AddSingleton<IMailDeliveryServiceConfiguration>(mailDeliveryServiceConfiguration);

            services.AddDbContext<RentalCarDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
