using NotificationService.Application.Interfaces;
using NotificationService.Application.Settings;
using RabbitMQ.Client;

namespace NotificationService.RabbitQueue
{
    public class RabbitHutch
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _channel;
        public static IBus CreateBus(RabbitMQSettings rabbitMqSettings)
        {
            var hostName = rabbitMqSettings.HostName;
            var userName = rabbitMqSettings.UserName;
            var password = rabbitMqSettings.Password;
            
            _factory = new ConnectionFactory 
            { 
                UserName = userName,
                Password = password,
                HostName = hostName, 
                DispatchConsumersAsync = true
            };
            
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            return new RabbitBus(_channel);
        }
    }
}