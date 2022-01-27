using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using RentalSystem.Api.AuthorizationPolicies;

namespace RentalSystem.Api.DependencyInjection
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationWithAuthorizationSupport(this IServiceCollection services, IConfiguration config)
        {
            services.AddMicrosoftIdentityWebApiAuthentication(config, "AzureAdB2C");
            services.AddSingleton<IAuthorizationHandler, ScopesHandler>();
            services.AddTransient<IAuthorizationHandler, RoleHandler>();

            return services;
        }
    }
}
