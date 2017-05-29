using System;
using System.Collections.Generic;
using System.Text;

namespace UniRepository.Interfaces
{
    public interface IEntity<TKey>
        where TKey : struct
    {
        TKey Id { get; set; }
    }
}
