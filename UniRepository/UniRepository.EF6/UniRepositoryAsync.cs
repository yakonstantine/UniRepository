using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using UniRepository.Core.Interfaces;

namespace UniRepository.EF6
{
    internal class UniRepositoryAsync<TEntity, TKey> : BaseUniRepository<TEntity, TKey>, IUniRepositoryAsync<TEntity, TKey>
        where TKey : struct
        where TEntity : class, IEntity<TKey>
    {
        private Func<DbContext> _GetDbContext;

        public UniRepositoryAsync(Func<DbContext> GetDbContext)
        {
            _GetDbContext = GetDbContext;
        }

        public new IUniRepositoryAsync<TEntity, TKey> Include(string path)
            => (IUniRepositoryAsync<TEntity, TKey>)base.Include(path);

        public new IUniRepositoryAsync<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression)
            => (IUniRepositoryAsync<TEntity, TKey>)base.Include(expression);

        public IQueryable<TEntity> GetAll()
        {
            return GetAll(_GetDbContext?.Invoke());
        }

        public async Task<TEntity> FindByKeyAsync(TKey key)
        {
            return await FindByKeyAsync(key, _GetDbContext?.Invoke());
        }

        public async Task SaveAsync(TEntity entity)
        {
            using (var dbContext = _GetDbContext?.Invoke())
            {
                var target = await FindByKeyAsync(entity.Id, dbContext);

                if (target == null)
                {
                    dbContext.Set<TEntity>().Add(entity);
                }
                else
                {
                    target.UpdateFrom(entity);
                }

                await dbContext.SaveChangesAsync();

                if (target != null)
                    entity.UpdateFrom(target);
            }
        }

        public async Task RemoveAsync(TKey key)
        {
            using (var dbContext = _GetDbContext?.Invoke())
            {
                var entity = await FindByKeyAsync(key, dbContext);

                dbContext.Set<TEntity>().Remove(entity);

                await dbContext.SaveChangesAsync();
            }
        }

        private async Task<TEntity> FindByKeyAsync(TKey key, DbContext dbContext)
        {
            return await base.FindByKey(key, dbContext)
                .SingleOrDefaultAsync();
        }
    }
}
