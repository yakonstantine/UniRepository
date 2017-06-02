using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniRepository.Core.Interfaces;

namespace UniRepository.EF7
{
    public abstract class BaseUniRepositoryManager : IUniRepositoryManager
    {
        protected abstract DbContext GetDBContext();

        public IUniRepository<TEntity, TKey> GetRepoFor<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : struct
            => new UniRepository<TEntity, TKey>(GetDBContext());

        public IUniRepositoryAsync<TEntity, TKey> GetAsyncRepoFor<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : struct
            => new UniRepositoryAsync<TEntity, TKey>(GetDBContext);
    }
}
