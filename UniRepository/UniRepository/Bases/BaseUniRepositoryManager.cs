using System;
using System.Collections.Generic;
using System.Text;
using UniRepository.Interfaces;

namespace UniRepository.Bases
{
    public abstract class BaseUniRepositoryManager : IUniRepositoryManager
    {
        //protected abstract DBContext GetDBContext();

        public IUniRepository<TEntity, TKey> GetFor<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : struct
        {
            throw new NotImplementedException();
        }

        public IUniRepositoryAsync<TEntity, TKey> GetForAsync<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : struct
        {
            throw new NotImplementedException();
        }
    }
}
