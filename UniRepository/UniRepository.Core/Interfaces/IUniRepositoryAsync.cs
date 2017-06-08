using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UniRepository.Core.Interfaces
{
    public interface IUniRepositoryAsync<TEntity, TKey>
        where TKey : struct
        where TEntity : class, IEntity<TKey>
    {
        IUniRepositoryAsync<TEntity, TKey> Include(string path);

        IUniRepositoryAsync<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression);

        IQueryable<TEntity> GetAll();

        Task<TEntity> FindByKeyAsync(TKey key);

        Task SaveAsync(TEntity entity);

        Task RemoveAsync(TKey key);
    }
}
