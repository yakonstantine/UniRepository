using System;
using System.Linq;
using System.Linq.Expressions;

namespace UniRepository.Core.Interfaces
{
    public interface IUniRepository<TEntity, TKey> : IDisposable
        where TKey : struct
        where TEntity : class, IEntity<TKey>
    {
        IUniRepository<TEntity, TKey> Include(string path);

        IUniRepository<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression);

        IQueryable<TEntity> GetAll();

        TEntity FindByKey(TKey key);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void SaveChanges();
    }
}
