using System;
using UniRepository.Core.Interfaces;
using UniRepository.EF6.Tests.Mapper;

namespace UniRepository.EF6.Tests.Mock.DomainModel
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
        where TKey : struct
    {
        public TKey Id { get; set; }

        public void UpdateFrom(IEntity<TKey> entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Input argument is null.");

            if (this.GetType() != entity.GetType())
                throw new ArgumentException($"Input argument should be type of {this.GetType().Name}");

            AutoMapperWrapper.Map(entity, this);
        }
    }
}
