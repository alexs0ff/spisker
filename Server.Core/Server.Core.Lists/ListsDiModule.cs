using Autofac;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Lists.GraphQL;
using Server.Core.Lists.GraphQL.Resolvers;
using Server.Core.Lists.Repositories;

namespace Server.Core.Lists
{
    /// <summary>
    /// DI модуль для регистрации типов списков.
    /// </summary>
    public class ListsDiModule: Module
    {
        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ListRepository>().As<IListRepository>();
            builder.RegisterType<ListItemRepository>().As<IListItemRepository>();
            builder.RegisterType<UserListsResolver>().As<IListResolver>();
            builder.RegisterType<AddNewListResolver>().As<IAddNewListResolver>();
        }
    }
}
