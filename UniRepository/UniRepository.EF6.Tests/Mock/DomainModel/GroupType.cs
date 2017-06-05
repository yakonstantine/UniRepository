using System;
using UniRepository.Core.Interfaces;

namespace UniRepository.EF6.Tests.Mock.DomainModel
{
    public class GroupType : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
