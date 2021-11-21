using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces.Clients;

namespace NotificationService.Infrastructure.Clients
{
    public static class ServiceRegistration
    {
        public static void AddClients(this IServiceCollection services)
        {
            services.AddTransient<ITelegramClient, TelegramClient>();
            services.AddTransient<ISmsCClient, SmsCClient>();
            services.AddTransient<IEmailClient, EmailClient>();
        }
    }
}