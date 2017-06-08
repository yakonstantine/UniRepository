using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UniRepository.Core.Interfaces;
using UniRepository.EF6.Tests.Mock;
using UniRepository.EF6.Tests.Mock.DomainModel;

namespace UniRepository.EF6.Tests.UniRepositoryAsyncTests.Bases
{
    public class BaseIntegrationAsyncTest
    {
        private IUniRepositoryManager _repositoryManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _repositoryManager = new MockUniRepositoryManager();
        }

        protected IUniRepositoryAsync<User, Guid> GetUserRepository()
        {
            return _repositoryManager.GetRepositoryAsyncFor<User, Guid>();
        }

        protected IUniRepositoryAsync<Tag, int> GetTagRepository()
        {
            return _repositoryManager.GetRepositoryAsyncFor<Tag, int>();
        }

        protected IUniRepositoryAsync<UsersGroup, Guid> GetUsersGroupRepository()
        {
            return _repositoryManager.GetRepositoryAsyncFor<UsersGroup, Guid>();
        }

        protected IUniRepositoryAsync<Document, int> GetDocumentRepository()
        {
            return _repositoryManager.GetRepositoryAsyncFor<Document, int>();
        }
    }
}
