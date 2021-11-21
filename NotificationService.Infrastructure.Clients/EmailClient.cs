using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Settings;

namespace NotificationService.Infrastructure.Clients
{
    public class EmailClient : IEmailClient
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailClient(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings?.Value;
        }

        public async Task SendMessage(string email, string body, string subject = null)
        {
            var host = _smtpSettings.Host;
            var port = _smtpSettings.Port;
            var from = _smtpSettings.EmailFrom;
            var password = _smtpSettings.Password;
            var mailMessage = new MailMessage(from, email, subject, body);

            using var client = new SmtpClient(host, port);

            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from, password);
            client.EnableSsl = true;

            client.Send(mailMessage);
        }
    }
}