using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniRepository.EF6.Tests.Mock.DomainModel
{
    public class UsersGroup : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
