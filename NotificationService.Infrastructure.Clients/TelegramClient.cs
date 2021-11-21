using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Settings;

namespace NotificationService.Infrastructure.Clients
{
    public class TelegramClient : ITelegramClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TelegramSettings _telegramSettings;

        public TelegramClient(IHttpClientFactory httpClientFactory, IOptions<TelegramSettings> telegramSettings)
        {
            _httpClientFactory = httpClientFactory;
            _telegramSettings = telegramSettings?.Value;
        }
        
        public async Task SendMessage(string chatId, string text)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var token = _telegramSettings.BotToken;
            var domain = _telegramSettings.Domain;

            using var response = await httpClient
                .GetAsync($"{domain}/bot{token}/sendMessage?" +
                          $"chat_id={chatId}&" +
                          $"text={text}");
        }
    }
}