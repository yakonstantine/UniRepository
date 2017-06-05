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
    internal class UniRepositoryAsync<TEntity, TKey> : IUniRepositoryAsync<TEntity, TKey>
        where TKey : struct
        where TEntity : class, IEntity<TKey>
    {
        private Func<DbContext> _GetDbContext;
        private IList<string> _includeProperties = new List<string>();

        public UniRepositoryAsync(Func<DbContext> GetDbContext)
        {
            _GetDbContext = GetDbContext;
        }

        public IUniRepositoryAsync<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var propertyName = GetPropertyName(expression);

            if (_includeProperties.Contains(propertyName))
                return this;

            _includeProperties.Add(propertyName);

            return this;
        }

        public IQueryable<TEntity> GetAll()
        {
            return GetAll(_GetDbContext());
        }

        public async Task<TEntity> FindByKeyAsync(TKey key)
        {
            return await FindByKeyAsync(key, _GetDbContext());
        }

        public async Task SaveAsync(TEntity entity)
        {
            using (var dbContext = _GetDbContext())
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

                entity.UpdateFrom(target);
            }
        }

        public async Task RemoveAsync(TKey key)
        {
            using (var dbContext = _GetDbContext())
            {
                var entity = await FindByKeyAsync(key, dbContext);

                dbContext.Set<TEntity>().Remove(entity);

                await dbContext.SaveChangesAsync();
            }
        }

        private IQueryable<TEntity> GetAll(DbContext dbContext)
        {
            IQueryable<TEntity> queryableCollection = dbContext.Set<TEntity>();

            foreach (var propertyName in _includeProperties)
            {
                queryableCollection = queryableCollection.Include(propertyName);
            }

            _includeProperties.Clear();

            return queryableCollection;
        }

        private async Task<TEntity> FindByKeyAsync(TKey key, DbContext dbContext)
        {
            return await GetAll(dbContext)
                    .Where(x => x.Id.Equals(key))
                    .SingleOrDefaultAsync();
        }

        private string GetPropertyName<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException($"Expression '{expression.ToString()}' refers to a method, not a property.");

            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo == null)
                throw new ArgumentException($"Expression '{expression.ToString()}' refers to a field, not a property.");

            var type = typeof(TEntity);

            if (type != propertyInfo.ReflectedType && !type.IsSubclassOf(propertyInfo.ReflectedType))
                throw new ArgumentException($"Expresion '{expression.ToString()}' refers to a property that is not from type {type}.");

            return propertyInfo.Name;
        }
    }
}
