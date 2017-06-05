using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRepository.Interfaces;

namespace UniRepository
{
    internal class UniRepositoryAsync<TEntity, TKey> : IUniRepositoryAsync<TEntity, TKey>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        private IDbContextAdapter _dbContextAdapter;

        public UniRepositoryAsync(IDbContextAdapter dbContextAdapter)
        {
            _dbContextAdapter = dbContextAdapter;
        }

        public Task<TEntity> FindByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IUniRepositoryAsync<TEntity, TKey> Include<TProperty>(System.Linq.Expressions.Expression<Func<TEntity, TProperty>> expression)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
