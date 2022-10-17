using HashHandler.Shared.Entities;
using HashHandler.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HashHandler.Shared.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private ApplicationContext RepositoryContext { get; }

        protected RepositoryBase(ApplicationContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public async Task CreateRangeAsync(T[] entities, CancellationToken cancellationToken)
        {
            await RepositoryContext.Set<T>().AddRangeAsync(entities, cancellationToken);
            await RepositoryContext.SaveChangesAsync(cancellationToken);
        }
    }
}