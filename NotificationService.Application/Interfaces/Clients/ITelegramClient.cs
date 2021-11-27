using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces.Clients
{
    public interface ITelegramClient
    {
        Task<bool> SendMessage(string chatId, string text);
    }
}