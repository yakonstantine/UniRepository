namespace UniRepository.EF6.Tests.Mock.DomainModel
{
    using System.Data.Common;
    using System.Data.Entity;

    public class MockModel : DbContext
    {
        public MockModel(DbConnection connection)
            : base(connection, true)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UsersGroup> UsersGroups { get; set; }

        public virtual DbSet<GroupType> GroupTypes { get; set; }
    }
}