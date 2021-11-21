using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Settings;

namespace NotificationService.Infrastructure.Clients
{
    public class SmsCClient: ISmsCClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SmscSettings _smscSettings;

        public SmsCClient(IHttpClientFactory httpClientFactory, IOptions<SmscSettings> smscSettings)
        {
            _httpClientFactory = httpClientFactory;
            _smscSettings = smscSettings?.Value;
        }
        
        public async Task SendMessage(string phoneNumber, string text)
        {
            var httpClient = _httpClientFactory.CreateClient();
            
            var result = await httpClient.PostAsync($"{_smscSettings.Url}/send.php?",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "login", _smscSettings.Login },
                    { "psw", _smscSettings.Password },
                    { "phones", phoneNumber },
                    { "mes", text }
                }));
            
            // var response = await result.Content.ReadAsStringAsync();
        }
    }
}