namespace HashHandler.Shared.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        Task CreateRangeAsync(T[] entities, CancellationToken cancellationToken);
    }
}