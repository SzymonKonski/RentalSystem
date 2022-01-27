using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using RentalSystem.MailSender.Infrastructure.Configuration.Interfaces;
using RentalSystem.MailSender.Infrastructure.Services.MsGraph;
using RentalSystem.MailSender.Infrastructure.Services.MsGraph.Interfaces;

namespace RentalSystem.MailSender.FuncApp.DependencyInjection
{
    internal static class UserManagementServiceCollectionExtensions
    {
        public static IServiceCollection AddUserManagementServices(this IServiceCollection services)
        {
            services.AddScoped<IGraphServiceClient>(implementationFactory =>
            {
                var msGraphServiceConfiguration = implementationFactory.GetRequiredService<IMsGraphServiceConfiguration>();
                IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(msGraphServiceConfiguration.AppId)
                    .WithTenantId(msGraphServiceConfiguration.TenantId)
                    .WithClientSecret(msGraphServiceConfiguration.AppSecret)
                    .Build();

                ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
                return new GraphServiceClient(authProvider);
            });

            services.AddScoped<IMsGraphSdkClientService, MsGraphSdkClientService>();

            return services;
        }
    }
}
