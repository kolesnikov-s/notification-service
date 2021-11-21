using System;
using System.Threading.Tasks;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Interfaces.MessageServices;

namespace NotificationService.Infrastructure.Services
{
    public class EmailMessageService: IEmailMessageService
    {
        private readonly IEmailClient _emailClient;

        public EmailMessageService(IEmailClient emailClient)
        {
            _emailClient = emailClient;
        }
        
        public async Task SendMessage(string contact, string message)
        {
            await _emailClient.SendMessage(contact, message);
            
            Console.WriteLine($"Send Email Message: {contact} - {message}");
        }
    }
}