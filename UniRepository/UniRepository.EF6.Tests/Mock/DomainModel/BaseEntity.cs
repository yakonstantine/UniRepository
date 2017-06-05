using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRepository.Core.Interfaces;
using UniRepository.EF6.Tests.Mapper;

namespace UniRepository.EF6.Tests.Mock.DomainModel
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
        where TKey : struct
    {
        public TKey Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void UpdateFrom(IEntity<TKey> entity)
        {
            if (this.GetType() != entity.GetType())
                throw new ArgumentException($"Input argument should be type of {this.GetType().Name}");

            AutoMapperWrapper.Map(entity, this);
        }
    }
}
