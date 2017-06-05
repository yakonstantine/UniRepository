using System;
using System.Collections.Generic;
using System.Text;
using UniRepository.Interfaces;

namespace UniRepository
{
    public class UniRepositoryManager
    {
        private IDbContextAdapter _dbContextAdapter;

        public UniRepositoryManager(IDbContextAdapter dbContextAdapter)
        {
            _dbContextAdapter = dbContextAdapter;
        }

        public IUniRepository<TEntity, TKey> GetFor<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : struct
            => new UniRepository<TEntity, TKey>(_dbContextAdapter);

        public IUniRepositoryAsync<TEntity, TKey> GetForAsync<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : struct
            => new UniRepositoryAsync<TEntity, TKey>(_dbContextAdapter);
    }
}
