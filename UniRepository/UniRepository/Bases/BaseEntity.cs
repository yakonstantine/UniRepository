using System;
using System.Collections.Generic;
using System.Text;
using UniRepository.Interfaces;

namespace UniRepository.Bases
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
        where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
