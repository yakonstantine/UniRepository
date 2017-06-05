using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UniRepository.Interfaces
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
