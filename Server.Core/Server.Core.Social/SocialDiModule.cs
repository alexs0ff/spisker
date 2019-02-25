using Autofac;
using Server.Core.Common.Repositories.Social;
using Server.Core.Social.GraphQL;
using Server.Core.Social.GraphQL.Resolvers;
using Server.Core.Social.Repositories;

namespace Server.Core.Social
{
    /// <summary>
    /// Модуль для социального блока.
    /// </summary>
    public class SocialDiModule: Module
    {
        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserFollowingMapRepository>().As<IUserFollowingMapRepository>();
            builder.RegisterType<PortalUserProfileRespository>().As<IPortalUserProfileRespository>();
            builder.RegisterType<ListUserLikeMapRepository>().As<IListUserLikeMapRepository>();
            builder.RegisterType<GetUsersResolver>().As<IGetUsersResolver>();
        }
    }
}
