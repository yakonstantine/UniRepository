using System;
using System.Collections.Generic;
using System.Text;

namespace UniRepository.Interfaces
{
    public interface IUniRepositoryManager
    {
        IUniRepository<TEntity, TKey> GetFor<TEntity, TKey>()
            where TKey : struct
            where TEntity : IEntity<TKey>;

        IUniRepositoryAsync<TEntity, TKey> GetForAsync<TEntity, TKey>()
           where TKey : struct
           where TEntity : IEntity<TKey>;
    }
}
