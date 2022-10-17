using HashHandler.Shared.Entities;
using HashHandler.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HashHandler.Shared.Repositories
{
    public class HashesRepository : RepositoryBase<HashRecord>, IHashesRepository
    {
        public HashesRepository(ApplicationContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<HashRecord[]> GetHashesAsync(CancellationToken cancellationToken)
        {
            return await FindAll()
                .OrderBy(hr => hr.Date)
                .ToArrayAsync(cancellationToken);
        }

        public async Task SaveHashesAsync(HashRecord[] hashes, CancellationToken cancellationToken)
        {
            await CreateRangeAsync(hashes, cancellationToken);
        }
    }
}