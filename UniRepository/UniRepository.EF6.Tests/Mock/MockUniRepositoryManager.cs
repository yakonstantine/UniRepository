using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRepository.EF6.Tests.Mock.DomainModel;

namespace UniRepository.EF6.Tests.Mock
{
    public class MockUniRepositoryManager : BaseUniRepositoryManager
    {
        private DbConnection _effortConnection;

        public MockUniRepositoryManager()
        {
            _effortConnection = Effort.DbConnectionFactory.CreateTransient();
        }

        protected override DbContext GetDBContext()
        {
            var context = new MockModel(_effortConnection);
            context.Database.CreateIfNotExists();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            return context;
        }
    }
}
