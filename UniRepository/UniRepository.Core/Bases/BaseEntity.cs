using UniRepository.Core.Interfaces;

namespace UniRepository.Core.Bases
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
        where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
