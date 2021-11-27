using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Models;
using NotificationService.Entities;
using NotificationService.RabbitQueue;

namespace NotificationService.Web.Controllers
{
    [ApiController]
    [Route("")]
    public class NotificationController: ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IBus _busControl;
        public NotificationController(IMessageService messageService, IBus busControl)
        {
            _messageService = messageService;
            _busControl = busControl;
        }
        
        [HttpPost, Route("message")]
        public async Task<IActionResult> SendMessage(SendMessageRequest request)
        {
            var result = await _messageService.SendMessage(request.Type, request.Contact, request.Text);
            
            return Ok(result);
        }
        
        [HttpPost, Route("queue/message")]
        public async Task<IActionResult> SendQueueMessage(SendMessageRequest request)
        {
            await _busControl.SendAsync(Queue.MessageQueue, request);
            
            return Ok();
        }
        
        [HttpPost, Route("test/message")]
        public async Task<IActionResult> TestQueueMessage(SendMessageRequest request)
        {
            return Ok();
        }
    }
}