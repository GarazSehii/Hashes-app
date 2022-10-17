using HashHandler.Shared.Entities;

namespace HashHandler.Shared.Repositories.Interfaces
{
    public interface IHashesRepository: IRepositoryBase<HashRecord>
    {
        Task<HashRecord[]> GetHashesAsync(CancellationToken cancellationToken);
        Task SaveHashesAsync(HashRecord[] hashes, CancellationToken cancellationToken);
    }
}