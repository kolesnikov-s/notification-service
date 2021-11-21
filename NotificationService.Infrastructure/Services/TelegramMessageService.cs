using System;
using System.Threading.Tasks;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Interfaces.MessageServices;

namespace NotificationService.Infrastructure.Services
{
    public class TelegramMessageService : ITelegramMessageService
    {
        private readonly ITelegramClient _telegramClient;

        public TelegramMessageService(ITelegramClient telegramClient)
        {
            _telegramClient = telegramClient;
        }
        
        public async Task SendMessage(string contact, string message)
        {
            await _telegramClient.SendMessage(contact, message);
            
            Console.WriteLine($"Send Telegram Message: {contact} - {message}");
        }
    }
}