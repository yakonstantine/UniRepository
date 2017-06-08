using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using UniRepository.EF6.Tests.UniRepositoryAsyncTests.Bases;
using static UniRepository.EF6.Tests.Helpers.EntityCreator;

namespace UniRepository.EF6.Tests.UniRepositoryAsyncTests
{
    [TestClass]
    public class IncludeReferencePropertiesAsyncTests : BaseIntegrationAsyncTest
    {
        #region Related object is Tag

        [TestMethod]
        public async Task Async_CreateNewEntityWith_OneToMany_SaveWithInclude()
        {
            var userReposotory = GetUserRepository();

            var tag = CreateNewTag();

            var user = CreateNewUser();
            user.Tags.Add(tag);

            await userReposotory
                .Include(x => x.Tags)
                .SaveAsync(user);

            var actualUserWithIncludedTags = await userReposotory
                .Include(x => x.Tags)
                .FindByKeyAsync(user.Id);

            Assert.IsNotNull(actualUserWithIncludedTags);
            Assert.IsNotNull(actualUserWithIncludedTags.Tags);
            Assert.AreEqual(1, actualUserWithIncludedTags.Tags.Count);

            var actualTag = actualUserWithIncludedTags.Tags.First();

            Assert.AreNotEqual(0, actualTag.Id);
            Assert.AreEqual(tag.Id, actualTag.Id);
            Assert.AreEqual(tag.Name, actualTag.Name);

            var actualUserWithoutIncludedTags = await userReposotory
                .FindByKeyAsync(user.Id);

            Assert.IsNotNull(actualUserWithoutIncludedTags);
            Assert.IsNull(actualUserWithoutIncludedTags.Tags);
        }

        [TestMethod]
        public async Task Async_CreateNewEntityWith_OneToMany_SaveWithoutInclude()
        {
            var userReposotory = GetUserRepository();

            var tag = CreateNewTag();

            var user = CreateNewUser();
            user.Tags.Add(tag);

            await userReposotory.SaveAsync(user);

            var actualUserWithIncludedTags = await userReposotory
                .Include(x => x.Tags)
                .FindByKeyAsync(user.Id);

            Assert.IsNotNull(actualUserWithIncludedTags);
            Assert.IsNotNull(actualUserWithIncludedTags.Tags);
            Assert.AreEqual(1, actualUserWithIncludedTags.Tags.Count);

            var actualUserWithoutIncludedTag = await userReposotory
              .FindByKeyAsync(user.Id);

            Assert.IsNotNull(actualUserWithoutIncludedTag);
            Assert.IsNull(actualUserWithoutIncludedTag.Tags);
        }

        #endregion

        #region Related object is Document

        [TestMethod]
        public async Task Async_CreateNewEntityWith_OneToMany_RelatedObj_SaveWithInclude()
        {
            var userReposotory = GetUserRepository();

            var document = CreateNewDocument();

            var user = CreateNewUser();
            user.Documents.Add(document);

            await userReposotory
                .Include(x => x.Documents)
                .SaveAsync(user);

            var actualUserWithIncludedDocuments = await userReposotory
                .Include(x => x.Documents)
                .FindByKeyAsync(user.Id);

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
        public async Task Async_CreateNewEntityWith_ManyToMany_RelatedObj_SaveWithInclude()
        {
            var userReposotory = GetUserRepository();
            var userGroupRepository = GetUsersGroupRepository();

            var userGroup = CreateNewUserGroup();

            var user = CreateNewUser();
            user.UsersGroups.Add(userGroup);

            await userReposotory
                .Include(x => x.UsersGroups)
                .SaveAsync(user);

            var actualUser = await userReposotory
                .Include(x => x.UsersGroups)
                .FindByKeyAsync(user.Id);

            Assert.IsNotNull(actualUser);
            Assert.IsNotNull(actualUser.UsersGroups);
            Assert.AreEqual(1, actualUser.UsersGroups.Count);

            var userGroupFromActualUser = actualUser.UsersGroups.First();

            var actualUserGroup = await userGroupRepository
                .Include(x => x.Users)
                .FindByKeyAsync(userGroupFromActualUser.Id);

            Assert.IsNotNull(actualUserGroup);
            Assert.IsNotNull(actualUserGroup.Users);
            Assert.AreEqual(1, actualUserGroup.Users.Count);

            Assert.IsTrue(actualUserGroup.Users.Any(x => x.Id == actualUser.Id));
        }

        [TestMethod]
        public async Task Async_CreateNewEntityWith_ManyToMany_RelatedObjWithRelatedObj_SaveWithInclude()
        {
            var userReposotory = GetUserRepository();
            var userGroupRepository = GetUsersGroupRepository();

            var userGroup = CreateNewUserGroup();

            var user1 = CreateNewUser();
            user1.UsersGroups.Add(userGroup);

            var user2 = CreateNewUser();
            userGroup.Users.Add(user2);

            await userReposotory
                .Include(x => x.UsersGroups)
                .SaveAsync(user1);

            var actualUser = await userReposotory
                .Include(x => x.UsersGroups)
                .FindByKeyAsync(user1.Id);

            Assert.IsNotNull(actualUser);
            Assert.IsNotNull(actualUser.UsersGroups);
            Assert.AreEqual(1, actualUser.UsersGroups.Count);

            var userGroupFromActualUser = actualUser.UsersGroups.First();

            // It happens because we didn't call the Include method 
            // for users inside the user group when finding user by Id 
            Assert.AreEqual(1, userGroupFromActualUser.Users.Count);

            var actualUserGroup = await userGroupRepository
                .Include(x => x.Users)
                .FindByKeyAsync(userGroupFromActualUser.Id);

            Assert.IsNotNull(actualUserGroup);
            Assert.IsNotNull(actualUserGroup.Users);
            Assert.AreEqual(2, actualUserGroup.Users.Count);

            Assert.IsTrue(actualUserGroup.Users.Any(x => x.Id == actualUser.Id));
            Assert.IsTrue(actualUserGroup.Users.Any(x => x.Id == user2.Id));
        }

        [TestMethod]
        public async Task Async_CreateNewEntityWith_ManyToMany_RelatedObjWithRelatedObj_IncludeSecondLevelOfRelations_SaveWithInclude()
        {
            var userReposotory = GetUserRepository();
            var userGroupRepository = GetUsersGroupRepository();

            var userGroup = CreateNewUserGroup();

            var user1 = CreateNewUser();
            user1.UsersGroups.Add(userGroup);

            var user2 = CreateNewUser();
            userGroup.Users.Add(user2);

            await userReposotory
                .Include(x => x.UsersGroups)
                .SaveAsync(user1);

            var actualUser = await userReposotory
                .Include(x => x.UsersGroups)
                .Include("UsersGroups.Users")
                .FindByKeyAsync(user1.Id);

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

        /// <summary>
        /// If you want to update related collection (add or remove one),
        /// you should call the Include method when saving the updated entity,
        /// otherwise you'll add all related objects from the collection as new.
        /// (example in the next test)
        /// </summary>
        [TestMethod]
        public async Task Async_UpdateEntityWith_OneToMany_SaveWithInclude()
        {
            var userReposotory = GetUserRepository();

            var tag1Name = "Tag1Name";
            var tag2Name = "Tag2Name";

            // Create new user with one tag
            var user = CreateNewUser();
            user.Tags.Add(CreateNewTag(tag1Name));

            // Insert user into db with related obj
            await userReposotory.SaveAsync(user);

            // Find user by Id
            var userFromDbWithIncludedTag = await userReposotory
                .Include(x => x.Tags)
                .FindByKeyAsync(user.Id);

            // Add new tag to user from db
            userFromDbWithIncludedTag.Tags.Add(CreateNewTag(tag2Name));

            // Update user with included property Tags
            await userReposotory
                .Include(x => x.Tags)
                .SaveAsync(userFromDbWithIncludedTag);

            // Find user by Id after update
            var actualUserWithIncludedTags = await userReposotory
                .Include(x => x.Tags)
                .FindByKeyAsync(user.Id);

            // Should add tag2 to collection of tags of the user entity 
            Assert.IsNotNull(actualUserWithIncludedTags);
            Assert.IsNotNull(actualUserWithIncludedTags.Tags);
            Assert.AreEqual(2, actualUserWithIncludedTags.Tags.Count);

            Assert.IsTrue(actualUserWithIncludedTags.Tags.Any(x => x.Name == tag1Name));
            Assert.IsTrue(actualUserWithIncludedTags.Tags.Any(x => x.Name == tag2Name));
        }

        /// <summary>
        /// If you call the Save method without calling the Include method,
        /// you'll add all related objects from the collection as new.
        /// </summary>
        [TestMethod]
        public async Task Async_UpdateEntityWith_OneToMany_SaveWithoutInclude()
        {
            var userReposotory = GetUserRepository();

            var tag1Name = "Tag1Name";
            var tag2Name = "Tag2Name";

            // Create new user with one tag
            var user = CreateNewUser();
            user.Tags.Add(CreateNewTag(tag1Name));

            // Insert user into db with related obj
            await userReposotory.SaveAsync(user);

            // Find user by Id
            var userFromDbWithIncludedTag = await userReposotory
                .Include(x => x.Tags)
                .FindByKeyAsync(user.Id);

            // Add new tag to user from db
            userFromDbWithIncludedTag.Tags.Add(CreateNewTag(tag2Name));

            // Update user without included property Tags
            await userReposotory
                .SaveAsync(userFromDbWithIncludedTag);

            // Find user by Id after update
            var actualUserWithIncludedTags = await userReposotory
                .Include(x => x.Tags)
                .FindByKeyAsync(user.Id);


            Assert.IsNotNull(actualUserWithIncludedTags);
            Assert.IsNotNull(actualUserWithIncludedTags.Tags);

            // In the related collection we have three objects.
            // It happens because we didn't call the Include metod.           
            Assert.AreEqual(3, actualUserWithIncludedTags.Tags.Count);
            // We have two tags with name 'Tag1Name' (the one old, added during the save,
            // and the one new, added during the update).
            Assert.AreEqual(2, actualUserWithIncludedTags.Tags.Count(x => x.Name == tag1Name));
            // And one tag with name 'Tag2Name', added during the update.
            Assert.AreEqual(1, actualUserWithIncludedTags.Tags.Count(x => x.Name == tag2Name));
        }
    }
}
