using EmailMessageProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Interfaces.MessageServices;
using NotificationService.Infrastructure.Services;
using SmsMessageProvider;
using TelegramMessageProvider;

namespace NotificationService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITelegramMessageService, TelegramMessageService>();
            services.AddTransient<ISmsMessageService, SmsMessageService>();
            services.AddTransient<IEmailMessageService, EmailMessageService>();
            services.AddTransient<IMessageService, MessageService>();
        }
    }
}