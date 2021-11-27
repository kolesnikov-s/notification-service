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
    public class EmailMessageService : IEmailMessageService
    {
        private readonly ILogger<EmailMessageService> _logger;
        private readonly IEmailClient _emailClient;
        private readonly IDbContextFactory<NotificationDbContext> _contextFactory;
        public EmailMessageService(
            ILogger<EmailMessageService> logger,
            IDbContextFactory<NotificationDbContext> contextFactory,
            IEmailClient emailClient)
        {
            _logger = logger;
            _emailClient = emailClient;
            _contextFactory = contextFactory;
        }

        public async Task<Response<Guid>> SendMessage(string contact, string text)
        {
            var isSent = await _emailClient.SendMessage(contact, text);

            await using var context = _contextFactory.CreateDbContext();

            var entity = new EmailMessage
            {
                Email = contact,
                Body = text,
                IsSent = isSent
            };
            
            await context.EmailMessages.AddAsync(entity);
            await context.SaveChangesAsync();

            var responseMessage = isSent ? SentMessageInfo.Success : SentMessageInfo.Error;

            _logger.LogInformation($"{responseMessage} - id: {entity.Id}");

            return new Response<Guid>(entity.Id, responseMessage);
        }
    }
}