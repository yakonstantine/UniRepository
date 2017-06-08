using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using UniRepository.Core.Interfaces;

namespace UniRepository.EF6
{
    public abstract class BaseUniRepositoryManager : IUniRepositoryManager
    {
        protected abstract DbContext GetDBContext();

        public IUniRepository<TEntity, TKey> GetRepositoryFor<TEntity, TKey>()
            where TKey : struct
            where TEntity : class, IEntity<TKey>
            => new UniRepository<TEntity, TKey>(GetDBContext());

        public IUniRepositoryAsync<TEntity, TKey> GetRepositoryAsyncFor<TEntity, TKey>()
            where TKey : struct
            where TEntity : class, IEntity<TKey>
            => new UniRepositoryAsync<TEntity, TKey>(GetDBContext);
    }
}
