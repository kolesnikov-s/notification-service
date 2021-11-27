using System;
using System.Threading.Tasks;
using NotificationService.Application;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Interfaces.MessageServices;
using NotificationService.Application.Wrappers;

namespace NotificationService.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly ISmsMessageService _smsMessageService;
        private readonly IEmailMessageService _emailMessageService;

        public MessageService(
            ITelegramMessageService telegramMessageService,
            ISmsMessageService smsMessageService,
            IEmailMessageService emailMessageService)
        {
            _telegramMessageService = telegramMessageService;
            _smsMessageService = smsMessageService;
            _emailMessageService = emailMessageService;
        }

        public async Task<Response<Guid>> SendMessage(string type, string contact, string text)
        {
            return await GetProviderByMessageType(type).SendMessage(contact, text);
        }

        private IMessageSender GetProviderByMessageType(string type)
        {
            IMessageSender provider = type?.ToLower() switch
            {
                MessageType.Sms => _smsMessageService,
                MessageType.Email => _emailMessageService,
                MessageType.Telegram => _telegramMessageService,
                _ => throw new Exception($"Message type {type} not found")
            };

            return provider;
        }
    }
}