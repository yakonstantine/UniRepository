using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniRepository.Interfaces
{
    public interface IDbContextAdapter : IDisposable
    {
        IQueryable<TEntity> Set<TEntity, TKey>()
            where TKey : struct
            where TEntity : IEntity<TKey>;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int Remove<TEntity, TKey>(TEntity entity)
            where TKey : struct
            where TEntity : IEntity<TKey>;

        Task<int> RemoveAsync<TEntity, TKey>(TEntity entity)
            where TKey : struct
            where TEntity : IEntity<TKey>;

        Task<int> RemoveAsync<TEntity, TKey>(TEntity entity, CancellationToken cancellationToken)
            where TKey : struct
            where TEntity : IEntity<TKey>;
    }
}
