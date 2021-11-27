using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Settings;

namespace NotificationService.Infrastructure.Clients
{
    public class TelegramClient : ITelegramClient
    {
        private readonly ILogger<TelegramClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TelegramSettings _telegramSettings;

        public TelegramClient(
            ILogger<TelegramClient> logger,
            IHttpClientFactory httpClientFactory,
            IOptions<TelegramSettings> telegramSettings)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _telegramSettings = telegramSettings?.Value;
        }

        public async Task<bool> SendMessage(string chatId, string text)
        {
            var token = _telegramSettings.BotToken;
            var domain = _telegramSettings.Domain;

            using var httpClient = _httpClientFactory.CreateClient();
            using var response = await httpClient
                .GetAsync($"{domain}/bot{token}/sendMessage?" +
                          $"chat_id={chatId}&" +
                          $"text={text}");

            bool isSent;

            try
            {
                response.EnsureSuccessStatusCode();
                isSent = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending message: {ex?.Message}");
                isSent = false;
            }

            return isSent;
        }
    }
}