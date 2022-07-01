using CommandAPIAlone.Models.Requests.EmailService;

namespace CommandAPIAlone.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailRequest emailRequest);
    }
}
