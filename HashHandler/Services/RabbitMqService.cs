using System.Text;
using HashHandler.Services.Interfaces;
using HashHandler.Shared.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HashHandler.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IOptions<RabbitMqOptions> _rabbitMqOptions;
        public RabbitMqService(IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _rabbitMqOptions = rabbitMqOptions;
        }
        
        public async Task SendMessage(string message, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var i = 0;
            
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqOptions.Value.HostName
            };
            
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMqOptions.Value.Queue,
                    durable: _rabbitMqOptions.Value.Durable,
                    exclusive: _rabbitMqOptions.Value.Exclusive,
                    autoDelete: _rabbitMqOptions.Value.AutoDelete,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: _rabbitMqOptions.Value.Exchange,
                    routingKey: _rabbitMqOptions.Value.Queue,
                    basicProperties: null,
                    body: body);
                i = 1;
            }

            await Task.FromResult(i);
        }
    }
}