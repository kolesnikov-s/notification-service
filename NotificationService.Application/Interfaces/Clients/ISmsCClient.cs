using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces.Clients
{
    public interface ISmsCClient
    {
        Task<bool> SendMessage(string phoneNumber, string text);
    }
}