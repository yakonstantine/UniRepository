using System;
using System.Collections.Generic;

namespace UniRepository.EF6.Tests.Mock.DomainModel
{
    public class User : BaseEntity<Guid>
    {
        public string FullName { get; set; }

        public int Number { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<UsersGroup> UsersGroups { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
