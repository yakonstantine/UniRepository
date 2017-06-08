using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UniRepository.EF6.Tests.Mock.DomainModel;
using UniRepository.EF6.Tests.UniRepositoryTests.Bases;
using static UniRepository.EF6.Tests.Helpers.EntityCreator;

namespace UniRepository.EF6.Tests.UniRepositoryTests
{
    [TestClass]
    public class SaveFindAndRemoveTests : BaseIntegrationTest
    {
        [TestMethod]
        public void CreateAndFindByKey_EntityWithGuidKey()
        {
            var userRepository = GetUserRepository();

            var key = Guid.NewGuid();

            var user = CreateNewUser(key);

            User actualUser = null; 

            try
            {
                userRepository.Add(user);
                userRepository.SaveChanges();

                actualUser = GetUserRepository().FindByKey(user.Id);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }            

            Assert.IsNotNull(actualUser);
            Assert.AreEqual(key, actualUser.Id);
            Assert.AreEqual(user.Id, actualUser.Id);
        }

        [TestMethod]
        public void CreateAndFindByKey_EntityWithIntKey()
        {
            var tagRepository = GetTagRepository();
            var tag = CreateNewTag();

            Tag actualTag = null;

            try
            {
                tagRepository.Add(tag);
                tagRepository.SaveChanges();

                actualTag = GetTagRepository().FindByKey(tag.Id);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            Assert.IsNotNull(actualTag);
            Assert.AreNotEqual(actualTag.Id, 0);
            Assert.AreEqual(tag.Id, actualTag.Id);
        }

        [TestMethod]
        public void CreateAndRemove_EntityWithGuidKey()
        {
            var userRepository1 = GetUserRepository();

            var key = Guid.NewGuid();

            var user = CreateNewUser(key);

            userRepository1.Add(user);
            userRepository1.SaveChanges();

            try
            {
                var userRepository2 = GetUserRepository();

                var userFromDB = userRepository2.FindByKey(user.Id);

                userRepository2.Remove(userFromDB);
                userRepository2.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            var actualUser = GetUserRepository().FindByKey(key);

            Assert.IsNull(actualUser);
        }

        [TestMethod]
        public void CreateAndRemove_EntityWithIntKey()
        {
            var tagRepository1 = GetTagRepository();
            var tag = CreateNewTag();

            tagRepository1.Add(tag);
            tagRepository1.SaveChanges();

            Assert.AreNotEqual(0, tag.Id);

            try
            {
                var tagRepository2 = GetTagRepository();

                var tagFromDB = tagRepository2.FindByKey(tag.Id);

                tagRepository2.Remove(tagFromDB);
                tagRepository2.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            var actualTag = GetTagRepository().FindByKey(tag.Id);

            Assert.IsNull(actualTag);
        }

        [TestMethod]
        public void CreateAndUpdateEntity()
        {
            var entityId = Guid.NewGuid();

            var oldName = "Old name";
            var newName = "New name";

            var userRepository1 = GetUserRepository();

            var user = CreateNewUser(entityId);
            user.Name = oldName;

            userRepository1.Add(user);
            userRepository1.SaveChanges();

            var userRepository2 = GetUserRepository();

            var userFromDb = userRepository2.FindByKey(entityId);
            userFromDb.Name = newName;

            userRepository2.SaveChanges();

            var actualUser = GetUserRepository().FindByKey(entityId);

            Assert.IsNotNull(actualUser);
            Assert.AreEqual(newName, actualUser.Name);
        }

        [TestMethod]
        public void ReturnEntityByFilter()
        {
            var userRepository = GetUserRepository();

            var user1 = CreateNewUser();
            user1.Name = "First User";
            var user2 = CreateNewUser();
            user2.Name = "Second User";

            userRepository.Add(user1);
            userRepository.Add(user2);
            userRepository.SaveChanges();

            var countOfUsers = GetUserRepository().GetAll().Count();

            Assert.AreEqual(2, countOfUsers);

            var actualUser = GetUserRepository()
                .GetAll()
                .FirstOrDefault(x => x.Name == user2.Name);

            Assert.IsNotNull(actualUser);
            Assert.AreEqual(user2.Id, actualUser.Id);
            Assert.AreEqual(user2.Name, actualUser.Name);
        }
    }
}
