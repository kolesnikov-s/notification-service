using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotificationService.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.RabbitQueue
{
    public class RabbitBus : IBus
    {
        private readonly IModel _channel;
        internal RabbitBus(IModel channel)
        {
            _channel = channel;   
        }
        public async Task SendAsync<T>(string queue, T message)
        {
            await Task.Run(() =>
            {
                _channel.QueueDeclare(queue, true, false, false);
                
                var properties = _channel.CreateBasicProperties();
                var output = JsonConvert.SerializeObject(message);

                properties.Persistent = false;
                
                _channel.BasicPublish(string.Empty, queue, null, 
                    Encoding.UTF8.GetBytes(output));
            });
        }
        public async Task ReceiveAsync<T>(string queue, Action<T>    onMessage)
        {
            _channel.QueueDeclare(queue, true, false, false);
            
            var consumer = new AsyncEventingBasicConsumer(_channel);
  
            consumer.Received += async (s, e) =>
            {
                var jsonSpecified = Encoding.UTF8.GetString(e.Body.Span);
                var item = JsonConvert.DeserializeObject<T>(jsonSpecified);
                onMessage(item);
                await Task.Yield();
            };
            
            _channel.BasicConsume(queue, true, consumer);
            
            await Task.Yield();
        }
    }
}