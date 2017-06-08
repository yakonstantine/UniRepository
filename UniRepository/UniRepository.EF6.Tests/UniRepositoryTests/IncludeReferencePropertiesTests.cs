using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UniRepository.EF6.Tests.UniRepositoryTests.Bases;
using static UniRepository.EF6.Tests.Helpers.EntityCreator;

namespace UniRepository.EF6.Tests.UniRepositoryTests
{
    [TestClass]
    public class IncludeReferencePropertiesTests : BaseIntegrationTest
    {
        #region Related object is Tag

        [TestMethod]
        public void CreateNewEntityWith_OneToMany_FindWithInclude()
        {
            var userReposotory = GetUserRepository();

            var tag = CreateNewTag();

            var user = CreateNewUser();
            user.Tags.Add(tag);

            userReposotory.Add(user);
            userReposotory.SaveChanges();

            var actualUserWithoutIncludedTags = GetUserRepository()
                .FindByKey(user.Id);

            Assert.IsNotNull(actualUserWithoutIncludedTags);
            Assert.IsNull(actualUserWithoutIncludedTags.Tags);

            var actualUserWithIncludedTags = GetUserRepository()
                .Include(x => x.Tags)
                .FindByKey(user.Id);

            Assert.IsNotNull(actualUserWithIncludedTags);
            Assert.IsNotNull(actualUserWithIncludedTags.Tags);
            Assert.AreEqual(1, actualUserWithIncludedTags.Tags.Count);        

            var actualTag = actualUserWithIncludedTags.Tags.First();

            Assert.AreNotEqual(0, actualTag.Id);
            Assert.AreEqual(tag.Id, actualTag.Id);
            Assert.AreEqual(tag.Name, actualTag.Name);
        }

        #endregion

        #region Related object is Document

        [TestMethod]
        public void CreateNewEntityWith_OneToMany_RelatedObj_FindWithInclude()
        {
            var userReposotory = GetUserRepository();

            var document = CreateNewDocument();

            var user = CreateNewUser();
            user.Documents.Add(document);

            userReposotory.Add(user);
            userReposotory.SaveChanges();

            var actualUserWithIncludedDocuments = GetUserRepository()
                .Include(x => x.Documents)
                .FindByKey(user.Id);

            Assert.IsNotNull(actualUserWithIncludedDocuments);
            Assert.IsNotNull(actualUserWithIncludedDocuments.Documents);
            Assert.AreEqual(1, actualUserWithIncludedDocuments.Documents.Count);

            var actualDocument = actualUserWithIncludedDocuments.Documents.First();

            Assert.AreNotEqual(0, actualDocument.Id);
            Assert.AreEqual(document.Id, actualDocument.Id);
            Assert.AreEqual(document.Name, actualDocument.Name);

            Assert.IsNotNull(actualDocument.User);
            Assert.AreEqual(user.Id, actualDocument.UserId);
        }

        #endregion

        #region Related object is UsersGroup

        [TestMethod]
        public void CreateNewEntityWith_ManyToMany_RelatedObj_FindWithInclude()
        {
            var userReposotory = GetUserRepository();
            var userGroup = CreateNewUserGroup();

            var user = CreateNewUser();
            user.UsersGroups.Add(userGroup);

            userReposotory.Add(user);
            userReposotory.SaveChanges();

            var actualUser = GetUserRepository()
                .Include(x => x.UsersGroups)
                .FindByKey(user.Id);

            Assert.IsNotNull(actualUser);
            Assert.IsNotNull(actualUser.UsersGroups);
            Assert.AreEqual(1, actualUser.UsersGroups.Count);

            var userGroupFromActualUser = actualUser.UsersGroups.First();

            var actualUserGroup = GetUsersGroupRepository()
                .Include(x => x.Users)
                .FindByKey(userGroupFromActualUser.Id);

            Assert.IsNotNull(actualUserGroup);
            Assert.IsNotNull(actualUserGroup.Users);
            Assert.AreEqual(1, actualUserGroup.Users.Count);

            Assert.IsTrue(actualUserGroup.Users.Any(x => x.Id == actualUser.Id));
        }

        [TestMethod]
        public void CreateNewEntityWith_ManyToMany_RelatedObjWithRelatedObj()
        {
            var userReposotory = GetUserRepository();

            var userGroup = CreateNewUserGroup();

            var user1 = CreateNewUser();
            user1.UsersGroups.Add(userGroup);

            var user2 = CreateNewUser();
            userGroup.Users.Add(user2);

            userReposotory.Add(user1);
            userReposotory.SaveChanges();

            var actualUser = GetUserRepository()
                .Include(x => x.UsersGroups)
                .FindByKey(user1.Id);

            Assert.IsNotNull(actualUser);
            Assert.IsNotNull(actualUser.UsersGroups);
            Assert.AreEqual(1, actualUser.UsersGroups.Count);

            var userGroupFromActualUser = actualUser.UsersGroups.First();

            // It happens because we didn't call the Include method 
            // for users inside the user group when finding user by Id
            Assert.AreEqual(1, userGroupFromActualUser.Users.Count);

            var actualUserGroup = GetUsersGroupRepository()
                .Include(x => x.Users)
                .FindByKey(userGroupFromActualUser.Id);

            Assert.IsNotNull(actualUserGroup);
            Assert.IsNotNull(actualUserGroup.Users);
            Assert.AreEqual(2, actualUserGroup.Users.Count);

            Assert.IsTrue(actualUserGroup.Users.Any(x => x.Id == actualUser.Id));
            Assert.IsTrue(actualUserGroup.Users.Any(x => x.Id == user2.Id));
        }

        [TestMethod]
        public void CreateNewEntityWith_ManyToMany_RelatedObjWithRelatedObj_IncludeSecondLevelOfRelations()
        {
            var userReposotory = GetUserRepository();

            var userGroup = CreateNewUserGroup();

            var user1 = CreateNewUser();
            user1.UsersGroups.Add(userGroup);

            var user2 = CreateNewUser();
            userGroup.Users.Add(user2);

            userReposotory.Add(user1);
            userReposotory.SaveChanges();

            var actualUser = GetUserRepository()
                .Include(x => x.UsersGroups)
                .Include("UsersGroups.Users")
                .FindByKey(user1.Id);

            Assert.IsNotNull(actualUser);
            Assert.IsNotNull(actualUser.UsersGroups);
            Assert.AreEqual(1, actualUser.UsersGroups.Count);

            var actualUserGroup = actualUser.UsersGroups.First();

            Assert.IsNotNull(actualUserGroup);
            Assert.IsNotNull(actualUserGroup.Users);
            Assert.AreEqual(2, actualUserGroup.Users.Count);

            Assert.IsTrue(actualUserGroup.Users.Any(x => x.Id == actualUser.Id));
            Assert.IsTrue(actualUserGroup.Users.Any(x => x.Id == user2.Id));
        }

        #endregion

        [TestMethod]
        public void UpdateEntityWith_OneToMany_SaveWithInclude()
        {
            var userReposotory1 = GetUserRepository();

            var tag1Name = "Tag1Name";
            var tag2Name = "Tag2Name";

            // Create new user with one tag
            var user = CreateNewUser();
            user.Tags.Add(CreateNewTag(tag1Name));

            // Insert user into db with related obj
            userReposotory1.Add(user);
            userReposotory1.SaveChanges();

            // Find user by Id
            var userReposotory2 = GetUserRepository();

            var userFromDbWithIncludedTag = userReposotory2
                .Include(x => x.Tags)
                .FindByKey(user.Id);

            // Add new tag to user from db
            userFromDbWithIncludedTag.Tags.Add(CreateNewTag(tag2Name));

            // Update user with included property Tags
            userReposotory2.SaveChanges();

            // Find user by Id after update
            var actualUserWithIncludedTags = GetUserRepository()
                .Include(x => x.Tags)
                .FindByKey(user.Id);

            // Should add tag2 to collection of tags of the user entity 
            Assert.IsNotNull(actualUserWithIncludedTags);
            Assert.IsNotNull(actualUserWithIncludedTags.Tags);
            Assert.AreEqual(2, actualUserWithIncludedTags.Tags.Count);

            Assert.IsTrue(actualUserWithIncludedTags.Tags.Any(x => x.Name == tag1Name));
            Assert.IsTrue(actualUserWithIncludedTags.Tags.Any(x => x.Name == tag2Name));
        }
    }
}
