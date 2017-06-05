using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniRepository.Core.Interfaces;
using UniRepository.EF6.Tests.Mock;
using UniRepository.EF6.Tests.Mock.DomainModel;

namespace UniRepository.EF6.Tests.UniRepositoryAsyncTests
{
    [TestClass]
    public class UnitTest1
    {
        private IUniRepositoryManager _repositoryManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _repositoryManager = new MockUniRepositoryManager();
        }

        [TestMethod]
        public async void Test1()
        {
            var userRepository = _repositoryManager.GetAsyncRepoFor<User, Guid>();

            var user = new User()
            {
                FullName = "User Userman",
                BirthDate = new DateTime(2000, 11, 22),
                Number = 123
            };

            try
            {
                await userRepository.SaveAsync(user);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            var actualUser = await userRepository.FindByKeyAsync(user.Id);

            Assert.IsNotNull(actualUser);
            Assert.AreEqual(user.Id, actualUser.Id);
        }
    }
}
