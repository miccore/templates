using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace  Miccore.Net.webapi_template.User.Api.Entities
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string html);
    }

    public class EmailService : IEmailService
    {

        private readonly string SmtpHost = "";
        private readonly string SmtpUser = "";
        private readonly int SmtpPort = 1;
        private readonly string SmtpPass = "";
        public EmailService()
        {
        }

        public void Send(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(SmtpUser, SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendText(string from, string to, string subject, string text)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = text };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(SmtpUser, SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}