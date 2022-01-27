using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using RentalSystem.MailSender.FuncApp.DependencyInjection;

[assembly: FunctionsStartup(typeof(RentalSystem.MailSender.FuncApp.Startup))]
namespace RentalSystem.MailSender.FuncApp
{
    internal class Startup : FunctionsStartup
    {
        private IConfiguration configuration;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureSettings();
            builder.Services.AddAppConfiguration(configuration);
            builder.Services.AddMailDeliveryServices();
            builder.Services.AddUserManagementServices();
            builder.Services.AddCarReservationMessagingService();
        }

        private void ConfigureSettings()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            configuration = config;
        }
    }
}
