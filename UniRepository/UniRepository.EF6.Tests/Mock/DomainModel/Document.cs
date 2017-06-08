using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniRepository.EF6.Tests.Mock.DomainModel
{
    public class Document : BaseEntity<int>
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
