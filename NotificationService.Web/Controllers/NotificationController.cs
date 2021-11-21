using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Interfaces;

namespace NotificationService.Web.Controllers
{
    [ApiController]
    [Route("")]
    public class NotificationController: ControllerBase
    {
        private readonly IMessageService _messageService;
        public NotificationController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        [HttpPost, Route("message")]
        public async Task<IActionResult> Message(string type, string contact, string message)
        {
            await _messageService.SendMessage(type, contact, message);
            
            return Ok();
        }
    }
}