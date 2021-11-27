using System;
using System.Threading.Tasks;
using NotificationService.Application.Wrappers;

namespace NotificationService.Application.Interfaces
{
    public interface IMessageSender
    {
        Task<Response<Guid>> SendMessage(string contact, string text);
    }
}