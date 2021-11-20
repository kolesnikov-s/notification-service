using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces
{
    public interface IMessageService
    {
        Task SendMessage(string type, string contact, string message);
    }
}