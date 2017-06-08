using UniRepository.EF6.Tests.Mock.DomainModel;

namespace UniRepository.EF6.Tests.Mapper
{
    public static class AutoMapperWrapper
    {
        static AutoMapperWrapper()
        {
            AutoMapper.Mapper.Initialize(x =>
            {
                x.CreateMap<Document, Document>();
                x.CreateMap<Tag, Tag>();
                x.CreateMap<User, User>();
                x.CreateMap<UsersGroup, UsersGroup>();

                x.ForAllMaps((typeMap, map) =>
                {
                    map.PreserveReferences();
                });
            });
        }

        public static T Map<T>(T sourse, T destination)
        {
            return AutoMapper.Mapper.Map(sourse, destination);
        } 
    }
}
