using System;
using System.Threading.Tasks;
using NotificationService.Application.Interfaces.MessageServices;

namespace TelegramMessageProvider
{
    public class TelegramMessageService : ITelegramMessageService
    {
        public async Task SendMessage(string contact, string message)
        {
            Console.WriteLine($"Send Telegram Message: {contact} - {message}");
        }
    }
}