using System.Threading.Tasks;
using RentalSystem.MailSender.Core.Entities;

namespace RentalSystem.MailSender.Infrastructure.Services.MsGraph.Interfaces
{
    public interface IMsGraphSdkClientService
    {
        Task<UserAccount> GetUserAsync(string userId);
    }
}
