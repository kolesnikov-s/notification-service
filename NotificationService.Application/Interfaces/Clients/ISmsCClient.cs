using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces.Clients
{
    public interface ISmsCClient
    {
        Task SendMessage(string phoneNumber, string text);
    }
}