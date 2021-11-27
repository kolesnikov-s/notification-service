using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Settings;

namespace NotificationService.Infrastructure.Clients
{
    public class SmsCClient : ISmsCClient
    {
        private readonly ILogger<SmsCClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SmscSettings _smscSettings;

        public SmsCClient(ILogger<SmsCClient> logger, IHttpClientFactory httpClientFactory,
            IOptions<SmscSettings> smscSettings)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _smscSettings = smscSettings?.Value;
        }

        public async Task<bool> SendMessage(string phoneNumber, string text)
        {
            var login = _smscSettings.Login;
            var password = _smscSettings.Password;
            var url = _smscSettings.Url;

            using var httpClient = _httpClientFactory.CreateClient();
            using var response = await httpClient
                .PostAsync($"{url}/send.php?",
                    new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "login", login },
                        { "psw", password },
                        { "phones", phoneNumber },
                        { "mes", text }
                    }));

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