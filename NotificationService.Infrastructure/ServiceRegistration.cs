using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Interfaces.MessageServices;
using NotificationService.Infrastructure.Services;

namespace NotificationService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ITelegramMessageService, TelegramMessageService>();
            services.AddTransient<ISmsMessageService, SmsMessageService>();
            services.AddTransient<IEmailMessageService, EmailMessageService>();
            services.AddTransient<IMessageService, MessageService>();
        }
    }
}