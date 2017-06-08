using System;
using System.Collections.Generic;
using UniRepository.EF6.Tests.Mock.DomainModel;

namespace UniRepository.EF6.Tests.Helpers
{
    public static class EntityCreator
    {
        public static User CreateNewUser(Guid? key = null)
        {
            return new User
            {
                Id = key ?? Guid.NewGuid(),
                Name = "User Userman",
                UsersGroups = new List<UsersGroup>(),
                Tags = new List<Tag>(),
                Documents = new List<Document>()
            };
        }

        public static Tag CreateNewTag(string name = null)
        {
            return new Tag
            {
                Name = name ?? "Tag for test"
            };
        }

        public static Document CreateNewDocument(string name = null)
        {
            return new Document
            {
                Name = name ?? "Document for test"
            };
        }

        public static UsersGroup CreateNewUserGroup(string name = null)
        {
            return new UsersGroup
            {
                Id = Guid.NewGuid(),
                Name = name ?? "UserGroup for test",
                Users = new List<User>()
            };
        }
    }
}
