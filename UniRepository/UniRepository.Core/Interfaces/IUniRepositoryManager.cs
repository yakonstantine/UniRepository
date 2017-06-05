using System;
using System.Collections.Generic;
using System.Text;

namespace UniRepository.Core.Interfaces
{
    public interface IUniRepositoryManager
    {
        IUniRepository<TEntity, TKey> GetRepoFor<TEntity, TKey>()
            where TKey : struct
            where TEntity : class, IEntity<TKey>;

        IUniRepositoryAsync<TEntity, TKey> GetAsyncRepoFor<TEntity, TKey>()
            where TKey : struct
            where TEntity : class, IEntity<TKey>;
    }
}
