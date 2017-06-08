using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UniRepository.Core.Interfaces;

namespace UniRepository.EF6
{
    internal class UniRepository<TEntity, TKey> : BaseUniRepository<TEntity, TKey>, IUniRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : class, IEntity<TKey>
    {
        private DbContext _dbContext;

        public UniRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public new IUniRepository<TEntity, TKey> Include(string path) 
            => (IUniRepository<TEntity, TKey>)base.Include(path);

        public new IUniRepository<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression) 
            => (IUniRepository<TEntity, TKey>)base.Include(expression);

        public TEntity FindByKey(TKey key)
        {
            return base.FindByKey(key, _dbContext)
                .SingleOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            return base.GetAll(_dbContext);
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
