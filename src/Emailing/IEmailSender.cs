using System.Threading.Tasks;

namespace Emailing
{
    public interface IEmailSender
    {
        Task SendMessage(EmailTemplate emailTemplate);
    }
}