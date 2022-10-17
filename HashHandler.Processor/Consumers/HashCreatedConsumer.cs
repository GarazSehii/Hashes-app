using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using HashHandler.Shared.Entities;
using HashHandler.Shared.Models.Domain;
using HashHandler.Shared.Repositories.Interfaces;
using HashHandler.Shared.Configuration;
using Microsoft.Extensions.Options;

namespace HashProcessor.Consumers;

public class HashCreatedConsumer : BackgroundService
{
    private readonly IServiceProvider _service;
    private readonly ILogger<HashCreatedConsumer> _logger;
    private readonly IModel _channel;
    private readonly IOptions<RabbitMqOptions> _rabbitMqOptions;

    public HashCreatedConsumer(IServiceProvider service, ILogger<HashCreatedConsumer> logger, IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _service = service;
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions;
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.Value.HostName
        };
        _channel = factory.CreateConnection().CreateModel();
        _channel.QueueDeclare(queue: _rabbitMqOptions.Value.Queue,
            durable: _rabbitMqOptions.Value.Durable,
            exclusive: _rabbitMqOptions.Value.Exclusive,
            autoDelete: _rabbitMqOptions.Value.AutoDelete,
            arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                if (string.IsNullOrEmpty(content))
                {
                    _logger.LogError("Message is invalid: {Message}", content);
                }

                var hashData = JsonSerializer.Deserialize<HashesData?>(content);

                if (hashData == null)
                {
                    _logger.LogError("Deserialized hashes is null: {@HashData}", hashData);
                }

                var hashes = hashData?.Hashes.Select(x => new HashRecord
                    {
                        Hash = x,
                        Date = hashData.Date.ToString("yyyy-MM-dd"),
                    })
                    .ToArray();

                if (hashes == null || !hashes.Any())
                {
                    _logger.LogError("Mapped hashes is null or empty: {@HashData}", hashData);
                }
                else
                {
                    using var scope = _service.CreateScope();
                    var hashesRepository = scope.ServiceProvider.GetRequiredService<IHashesRepository>();
                    await hashesRepository.SaveHashesAsync(hashes!, stoppingToken);
                }

                _channel.BasicAck(ea.DeliveryTag, _rabbitMqOptions.Value.Multiple);
            };

            _channel.BasicConsume(_rabbitMqOptions.Value.Queue, _rabbitMqOptions.Value.AutoAck, consumer);
            await Task.Delay(500, stoppingToken);
        }

        await Task.CompletedTask;
    }
}