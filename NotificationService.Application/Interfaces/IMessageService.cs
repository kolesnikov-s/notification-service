using System;
using System.Threading.Tasks;
using NotificationService.Application.Wrappers;

namespace NotificationService.Application.Interfaces
{
    public interface IMessageService
    {
        Task<Response<Guid>> SendMessage(string type, string contact, string text);
    }
}