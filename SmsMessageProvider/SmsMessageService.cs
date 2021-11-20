using System;
using System.Threading.Tasks;
using NotificationService.Application.Interfaces.MessageServices;

namespace SmsMessageProvider
{
    public class SmsMessageService : ISmsMessageService
    {
        public async Task SendMessage(string contact, string message)
        {
            Console.WriteLine($"Send SMS Message: {contact} - {message}");
        }
    }
}