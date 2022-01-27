using RentalSystem.MailSender.Core.Entities;
using System;
using System.Threading.Tasks;
using RentalSystem.MailSender.Infrastructure.Configuration.Interfaces;
using RentalSystem.MailSender.Infrastructure.Services.MsGraph.Interfaces;
using Microsoft.Graph;
using System.Linq;

namespace RentalSystem.MailSender.Infrastructure.Services.MsGraph
{
    public class MsGraphSdkClientService : IMsGraphSdkClientService
    {
        private readonly IMsGraphServiceConfiguration msGraphServiceConfiguration;
        private readonly IGraphServiceClient graphServiceClient;

        public MsGraphSdkClientService(IMsGraphServiceConfiguration msGraphServiceConfiguration,
            IGraphServiceClient graphServiceClient)
        {
            this.msGraphServiceConfiguration = msGraphServiceConfiguration
                                               ?? throw new ArgumentNullException(nameof(msGraphServiceConfiguration));

            this.graphServiceClient = graphServiceClient
                                      ?? throw new ArgumentNullException(nameof(graphServiceClient));
        }

        public async Task<UserAccount> GetUserAsync(string userId)
        {
            var user = await graphServiceClient.Users[userId]
                .Request()
                .Select(e => new
                {
                    e.Id,
                    e.GivenName,
                    e.Surname,
                    e.Identities
                })
                .GetAsync();

            if (user != null)
            {
                var email = user.Identities.ToList()
                    .FirstOrDefault(i => i.SignInType == "emailAddress")
                    ?.IssuerAssignedId;

                return new UserAccount
                {
                    Id = user.Id,
                    FirstName = user.GivenName,
                    LastName = user.Surname,
                    Email = email
                };
            }

            else
            {
                return null;
            }
        }
    }
}
