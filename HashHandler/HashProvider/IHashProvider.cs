using HashHandler.Shared.Models.Domain;

namespace HashHandler.HashProvider
{
    public interface IHashProvider
    {
        Task<HashesData> GenerateAsync(int hashQuantity, CancellationToken cancellationToken);
    }
}