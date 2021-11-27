using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Models;
using NotificationService.RabbitQueue;

namespace NotificationService.Infrastructure.BackgroundJobs
{
    public class QueueWorker : BackgroundService
    {
        private readonly ILogger<QueueWorker> _logger;
        private readonly IMessageService _messageService;
        private readonly IBus _busControl;

        public QueueWorker(ILogger<QueueWorker> logger, IMessageService messageService, IBus busControl)
        {
            _logger = logger;
            _messageService = messageService;
            _busControl = busControl;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _busControl.ReceiveAsync<SendMessageRequest>(Queue.MessageQueue,r => 
                Task.Run(() => RubJob(r), stoppingToken));
        }

        private void RubJob(SendMessageRequest request)
        {
            _messageService.SendMessage(request.Type, request.Contact, request.Text);
            
            _logger.LogInformation($"Send Message: {request.Contact} - {request.Text}");
        }
    }
}