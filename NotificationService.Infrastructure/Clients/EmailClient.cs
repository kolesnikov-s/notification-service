using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Settings;

namespace NotificationService.Infrastructure.Clients
{
    public class EmailClient : IEmailClient
    {
        private readonly ILogger<EmailClient> _logger;
        private readonly SmtpSettings _smtpSettings;

        public EmailClient(ILogger<EmailClient> logger, IOptions<SmtpSettings> smtpSettings)
        {
            _logger = logger;
            _smtpSettings = smtpSettings?.Value;
        }

        public async Task<bool> SendMessage(string email, string body, string subject = null)
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

            bool isSent;

            try
            {
                await client.SendMailAsync(mailMessage);
                isSent = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending message: {ex?.Message}");
                isSent = false;
            }

            return isSent;
        }
    }
}