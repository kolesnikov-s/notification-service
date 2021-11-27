using System.Threading.Tasks;

namespace NotificationService.Application.Interfaces.Clients
{
    public interface IEmailClient
    {
        Task<bool> SendMessage(string email, string body, string subject = null);
    }
}