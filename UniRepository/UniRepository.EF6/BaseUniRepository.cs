using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UniRepository.Core.Interfaces;

namespace UniRepository.EF6
{
    internal abstract class BaseUniRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : class, IEntity<TKey>
    {
        private IList<string> _includeProperties = new List<string>();

        protected BaseUniRepository<TEntity, TKey> Include(string path)
        {
            if (_includeProperties.Contains(path))
                return this;

            _includeProperties.Add(path);

            return this;
        }

        protected BaseUniRepository<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var propertyName = GetPropertyName(expression);

            return this.Include(propertyName);
        }

        protected IQueryable<TEntity> GetAll(DbContext dbContext)
        {
            IQueryable<TEntity> queryableCollection = dbContext.Set<TEntity>();

            foreach (var propertyName in _includeProperties)
            {
                queryableCollection = queryableCollection.Include(propertyName);
            }

            _includeProperties.Clear();

            return queryableCollection;
        }

        protected IQueryable<TEntity> FindByKey(TKey key, DbContext dbContext)
        {
            return this.GetAll(dbContext)
                .Where(x => x != null && x.Id.ToString() == key.ToString());
        }

        private static string GetPropertyName<TProperty>(Expression<Func<TEntity, TProperty>> expression)
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
