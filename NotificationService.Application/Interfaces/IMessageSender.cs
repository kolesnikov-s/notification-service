using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces
{
    public interface IMessageSender
    {
        Task SendMessage(string contact, string text);
    }
}