namespace UniRepository.Core.Interfaces
{
    public interface IUniRepositoryManager
    {
        IUniRepository<TEntity, TKey> GetRepositoryFor<TEntity, TKey>()
            where TKey : struct
            where TEntity : class, IEntity<TKey>;

        IUniRepositoryAsync<TEntity, TKey> GetRepositoryAsyncFor<TEntity, TKey>()
            where TKey : struct
            where TEntity : class, IEntity<TKey>;
    }
}
