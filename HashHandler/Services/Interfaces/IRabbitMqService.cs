namespace HashHandler.Services.Interfaces
{
    public interface IRabbitMqService
    {
        Task SendMessage(string message, CancellationToken cancellationToken);
    }
}