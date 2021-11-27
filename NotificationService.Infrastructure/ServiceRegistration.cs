using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Interfaces.MessageServices;
using NotificationService.Infrastructure.Clients;
using NotificationService.Infrastructure.EF;
using NotificationService.Infrastructure.Services;

namespace NotificationService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<NotificationDbContext>(option =>
                option.UseNpgsql(configuration.GetConnectionString("Default")));
            
            services.AddDbContext<NotificationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddTransient<ITelegramMessageService, TelegramMessageService>();
            services.AddTransient<ISmsMessageService, SmsMessageService>();
            services.AddTransient<IEmailMessageService, EmailMessageService>();
            services.AddTransient<IMessageService, MessageService>();
            
            services.AddTransient<ITelegramClient, TelegramClient>();
            services.AddTransient<ISmsCClient, SmsCClient>();
            services.AddTransient<IEmailClient, EmailClient>();
        }
    }
}