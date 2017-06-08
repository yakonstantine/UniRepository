using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UniRepository.EF6.Tests.UniRepositoryAsyncTests.Bases;
using static UniRepository.EF6.Tests.Helpers.EntityCreator;

namespace UniRepository.EF6.Tests.UniRepositoryAsyncTests
{
    [TestClass]
    public class SaveFindAndRemoveAsyncTests : BaseIntegrationAsyncTest
    {
        [TestMethod]
        public async Task Async_CreateAndFindByKey_EntityWithGuidKey()
        {
            var userRepository = GetUserRepository();

            var key = Guid.NewGuid();

            var user = CreateNewUser(key);

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
            Assert.AreEqual(key, actualUser.Id);
            Assert.AreEqual(user.Id, actualUser.Id);
        }

        [TestMethod]
        public async Task Async_CreateAndFindByKey_EntityWithIntKey()
        {
            var tagRepository = GetTagRepository();
            var tag = CreateNewTag();

            try
            {
                await tagRepository.SaveAsync(tag);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            var actualTag = await tagRepository.FindByKeyAsync(tag.Id);

            Assert.IsNotNull(actualTag);
            Assert.AreNotEqual(actualTag.Id, 0);
            Assert.AreEqual(tag.Id, actualTag.Id);
        }

        [TestMethod]
        public async Task Async_CreateAndRemove_EntityWithGuidKey()
        {
            var userRepository = GetUserRepository();

            var key = Guid.NewGuid();

            var user = CreateNewUser(key);

            try
            {
                await userRepository.SaveAsync(user);
                await userRepository.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            var actualUser = await userRepository.FindByKeyAsync(key);

            Assert.IsNull(actualUser);
        }

        [TestMethod]
        public async Task Async_CreateAndRemove_EntityWithIntKey()
        {
            var tagRepository = GetTagRepository();
            var tag = CreateNewTag();

            try
            {
                await tagRepository.SaveAsync(tag);

                Assert.AreNotEqual(0, tag.Id);

                await tagRepository.RemoveAsync(tag.Id);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            var actualTag = await tagRepository.FindByKeyAsync(tag.Id);

            Assert.IsNull(actualTag);
        }

        [TestMethod]
        public async Task Async_CreateAndUpdateEntity()
        {
            var entityId = Guid.NewGuid();

            var oldName = "Old name";
            var newName = "New name";

            var userRepository = GetUserRepository();

            var user = CreateNewUser(entityId);
            user.Name = oldName;

            await userRepository.SaveAsync(user);

            var userFromDb = await userRepository.FindByKeyAsync(entityId);
            userFromDb.Name = newName;

            await userRepository.SaveAsync(userFromDb);

            var actualUser = await userRepository.FindByKeyAsync(entityId);

            Assert.IsNotNull(actualUser);
            Assert.AreEqual(newName, actualUser.Name);
        }

        [TestMethod]
        public async Task Async_ReturnEntityByFilter()
        {
            var userRepository = GetUserRepository();

            var user1 = CreateNewUser();
            user1.Name = "First User";
            var user2 = CreateNewUser();
            user2.Name = "Second User";

            await userRepository.SaveAsync(user1);
            await userRepository.SaveAsync(user2);

            var countOfUsers = await userRepository.GetAll().CountAsync();

            Assert.AreEqual(2, countOfUsers);

            var actualUser = await userRepository.GetAll()
                .Where(x => x.Name == user2.Name)
                .FirstOrDefaultAsync();

            Assert.IsNotNull(actualUser);
            Assert.AreEqual(user2.Id, actualUser.Id);
            Assert.AreEqual(user2.Name, actualUser.Name);
        }
    }
}
