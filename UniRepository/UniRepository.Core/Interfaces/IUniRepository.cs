using System;
using System.Linq;
using System.Linq.Expressions;

namespace UniRepository.Core.Interfaces
{
    public interface IUniRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : class, IEntity<TKey>
    {
        IUniRepository<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression);

        IQueryable<TEntity> GetAll();

        TEntity FindByKey(TKey key);

        void Save(TEntity entity);

        void Remove(TKey key);
    }
}
