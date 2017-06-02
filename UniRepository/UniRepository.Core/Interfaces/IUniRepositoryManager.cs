using System;
using System.Collections.Generic;
using System.Text;

namespace UniRepository.Core.Interfaces
{
    public interface IUniRepositoryManager
    {
        IUniRepository<TEntity, TKey> GetRepoFor<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : struct;

        IUniRepositoryAsync<TEntity, TKey> GetAsyncRepoFor<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : struct;
    }
}
