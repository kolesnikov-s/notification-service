using System;
using System.Threading.Tasks;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Interfaces.MessageServices;

namespace NotificationService.Infrastructure.Services
{
    public class SmsMessageService : ISmsMessageService
    {
        private readonly ISmsCClient _smsCClient;
        public SmsMessageService(ISmsCClient smsCClient)
        {
            _smsCClient = smsCClient;
        }
        
        public async Task SendMessage(string contact, string message)
        {
            await _smsCClient.SendMessage(contact, message);
            
            Console.WriteLine($"Send SMS Message: {contact} - {message}");
        }
    }
}