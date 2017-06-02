using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UniRepository.Core.Interfaces
{
    public interface IUniRepositoryAsync<TEntity, TKey>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        IUniRepositoryAsync<TEntity, TKey> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression);

        IQueryable<TEntity> GetAll();

        Task<TEntity> FindByIdAsync(TKey id);

        Task SaveAsync(TEntity entity);

        Task RemoveAsync(TKey id);
    }
}
