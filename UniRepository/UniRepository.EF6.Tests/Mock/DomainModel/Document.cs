using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniRepository.Core.Interfaces;
using UniRepository.EF6.Tests.Mapper;

namespace UniRepository.EF6.Tests.Mock.DomainModel
{
    public class Document : BaseEntity<int>
    {
        public int Number { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
