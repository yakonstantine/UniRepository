using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UniRepository.Interfaces
{
    public interface IUniRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        IUniRepository<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression);

        IQueryable<TEntity> GetAll();

        TEntity FindById(TKey id);

        void Save(TEntity entity);

        void Remove(TKey id);
    }
}
