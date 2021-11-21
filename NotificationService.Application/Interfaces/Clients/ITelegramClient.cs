using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces.Clients
{
    public interface ITelegramClient
    {
        Task SendMessage(string chatId, string text);
    }
}