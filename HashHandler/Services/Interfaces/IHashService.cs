using HashHandler.Models;

namespace HashHandler.Services.Interfaces
{
    public interface IHashService
    {
        Task<HashesResponse> GetHashesAsync(CancellationToken cancellationToken);
        Task GenerateAndSendAsync(CancellationToken cancellationToken);
    }
}