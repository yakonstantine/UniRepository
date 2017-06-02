using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UniRepository.Core.Interfaces;

namespace UniRepository.EF7
{
    internal class UniRepository<TEntity, TKey> : IUniRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        private DbContext _dbContext;

        public UniRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
