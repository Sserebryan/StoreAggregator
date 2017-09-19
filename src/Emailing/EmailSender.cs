using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Emailing
{
    public class EmailSender: IEmailSender
    {
        private readonly EmailCredential _emailCredential;

        public EmailSender(IOptions<EmailCredential> emailOptions)
        {
            _emailCredential = emailOptions.Value;
        }
        
        public async Task SendMessage(EmailTemplate emailTemplate)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailCredential.UserName, _emailCredential.Email));
            message.To.Add(new MailboxAddress(emailTemplate.ReceiverName, emailTemplate.Receiver));
            message.Subject = emailTemplate.Subject;
           
            message.Body = new TextPart("plain")
            {
                Text = emailTemplate.Content
            };

            using (var client = new SmtpClient())
            {

                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_emailCredential.UserName, _emailCredential.Password);

                await client.SendAsync(message);
                
                client.Disconnect(true);
            }
        }
    }
}
