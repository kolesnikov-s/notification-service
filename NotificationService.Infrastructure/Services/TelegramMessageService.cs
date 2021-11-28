using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationService.Application;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Interfaces.MessageServices;
using NotificationService.Application.Wrappers;
using NotificationService.Entities;
using NotificationService.Infrastructure.EF;

namespace NotificationService.Infrastructure.Services
{
    public class TelegramMessageService : ITelegramMessageService
    {
        private readonly ILogger<TelegramMessageService> _logger;
        private readonly ITelegramClient _telegramClient;
        private readonly IDbContextFactory<NotificationDbContext> _contextFactory;

        public TelegramMessageService(
            ILogger<TelegramMessageService> logger,
            IDbContextFactory<NotificationDbContext> contextFactory,
            ITelegramClient telegramClient)
        {
            _logger = logger;
            _telegramClient = telegramClient;
            _contextFactory = contextFactory;
        }
        
        public async Task<Response<Guid>> SendMessage(string contact, string text)
        {
            var isSent = await _telegramClient.SendMessage(contact, text);
            
            await using var context = _contextFactory.CreateDbContext();

            var entity = new TelegramMessage
            {
                ChatId = contact,
                Text = text,
                IsSent = isSent
            };
            
            await context.TelegramMessages.AddAsync(entity);
            await context.SaveChangesAsync();

            var responseMessage = isSent ? SentMessageInfo.Success : SentMessageInfo.Error;

            _logger.LogInformation($"{responseMessage} - id: {entity.Id}");

            return new Response<Guid>(entity.Id, responseMessage);
        }
    }
}