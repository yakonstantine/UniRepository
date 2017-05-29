using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRepository.Interfaces;

namespace UniRepository
{
    internal class UniRepository<TEntity, TKey> : IUniRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        public TEntity FindById(TKey id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IUniRepository<TEntity, TKey> Include<TProperty>(System.Linq.Expressions.Expression<Func<TEntity, TProperty>> expression)
        {
            throw new NotImplementedException();
        }

        public void Remove(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Save(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
