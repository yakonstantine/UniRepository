using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UniRepository.Core.Interfaces;

namespace UniRepository.EF6
{
    internal class UniRepositoryAsync<TEntity, TKey> : IUniRepositoryAsync<TEntity, TKey>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        private Func<DbContext> _GetDbContext;

        public UniRepositoryAsync(Func<DbContext> GetDbContext)
        {
            _GetDbContext = GetDbContext;
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
