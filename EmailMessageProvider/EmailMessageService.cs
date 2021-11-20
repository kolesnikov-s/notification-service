using System;
using System.Threading.Tasks;
using NotificationService.Application.Interfaces.MessageServices;

namespace EmailMessageProvider
{
    public class EmailMessageService: IEmailMessageService
    {
        public async Task SendMessage(string contact, string message)
        {
            Console.WriteLine($"Send Email Message: {contact} - {message}");
        }
    }
}