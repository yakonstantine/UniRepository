using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UniRepository.Core.Interfaces;
using UniRepository.EF6.Tests.Mock;
using UniRepository.EF6.Tests.Mock.DomainModel;

namespace UniRepository.EF6.Tests.UniRepositoryTests.Bases
{
    public class BaseIntegrationTest
    {
        private IUniRepositoryManager _repositoryManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _repositoryManager = new MockUniRepositoryManager();
        }

        protected IUniRepository<User, Guid> GetUserRepository()
        {
            return _repositoryManager.GetRepositoryFor<User, Guid>();
        }

        protected IUniRepository<Tag, int> GetTagRepository()
        {
            return _repositoryManager.GetRepositoryFor<Tag, int>();
        }

        protected IUniRepository<UsersGroup, Guid> GetUsersGroupRepository()
        {
            return _repositoryManager.GetRepositoryFor<UsersGroup, Guid>();
        }

        protected IUniRepository<Document, int> GetDocumentRepository()
        {
            return _repositoryManager.GetRepositoryFor<Document, int>();
        }
    }
}
