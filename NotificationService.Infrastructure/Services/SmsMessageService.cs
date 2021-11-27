using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationService.Application;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Interfaces.MessageServices;
using NotificationService.Application.Wrappers;
using NotificationService.Entities;
using NotificationService.Infrastructure.EF;

namespace NotificationService.Infrastructure.Services
{
    public class SmsMessageService : ISmsMessageService
    {
        private readonly ILogger<SmsMessageService> _logger;
        private readonly ISmsCClient _smsCClient;
        private readonly IDbContextFactory<NotificationDbContext> _contextFactory;

        public SmsMessageService(
            ILogger<SmsMessageService> logger,
            IDbContextFactory<NotificationDbContext> contextFactory,
            ISmsCClient smsCClient)
        {
            _logger = logger;
            _smsCClient = smsCClient;
            _contextFactory = contextFactory;
        }
        
        public async Task<Response<Guid>> SendMessage(string contact, string text)
        {
            var isSent = await _smsCClient.SendMessage(contact, text);

            await using var context = _contextFactory.CreateDbContext();

            var entity = new SmsMessage
            {
                Phone = contact,
                Text = text,
                IsSent = isSent
            };
            
            await context.SmsMessages.AddAsync(entity);
            await context.SaveChangesAsync();

            var responseMessage = isSent ? SentMessageInfo.Success : SentMessageInfo.Error;

            _logger.LogInformation($"{responseMessage} - id: {entity.Id}");

            return new Response<Guid>(entity.Id, responseMessage);
        }
    }
}