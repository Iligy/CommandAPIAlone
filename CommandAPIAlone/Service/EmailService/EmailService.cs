using CommandAPIAlone.Interfaces.Services;
using CommandAPIAlone.Models.Requests.EmailService;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace CommandAPIAlone.Service.EmailService
{
    public class EmailService : IEmailService
    {
        public void SendEmail(EmailRequest emailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("amir.nader88@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailRequest.Body };


            using (var smtp = new SmtpClient()) 
            {
                // gmail
                // smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("amir.nader88@ethereal.email", "ANFjq61738Yg4UEmGW");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
