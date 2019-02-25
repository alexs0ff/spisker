using AutoMapper;
using Server.Core.Lists;
using Server.Core.Social;
using Server.Core.Users;

namespace Server.Core.RestApi.Infrastructure
{
    public static class MapperConfig
    {
        public static IMapper Create()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ListsMapperProfile>();
                cfg.AddProfile<SocialMapperProfile>();
                cfg.AddProfile<UsersMapperProfile>();
                cfg.AddProfile<ServerMapperProfile>();
            });

            IMapper mapper = new Mapper(config);
            return mapper;
        }
    }
}