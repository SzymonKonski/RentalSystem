using System.Threading.Tasks;
using RentalSystem.MailSender.Infrastructure.Services.Mail.Templates;

namespace RentalSystem.MailSender.Infrastructure.Services.Mail.Interfaces
{
    public interface IMailDeliveryService
    {
        Task SendInvitationMessageAsync(CarReservationConfirmationMailTemplate carReservationConfirmationMailTemplate);
    }
}
