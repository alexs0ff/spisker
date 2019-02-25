using Autofac;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Server.Core.Common.Contexts;
using Server.Core.Common.GraphQL;
using Server.Core.Common.Logger;
using Server.Core.Common.Settings.API;
using Server.Core.Common.Settings.Entities;
using Server.Core.Common.Settings.Files;
using Server.Core.Lists.GraphQL;
using Server.Core.RestApi.Infrastructure.Settings;
using Server.Core.RestApi.Models.Auth;
using Server.Core.Social.GraphQL;

namespace Server.Core.RestApi.Infrastructure
{
    public class ServerModule : Module
    {
        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Logger.Logger>().As<IServerLogger>();
            RegisterGraphQl(builder);
        }

        private void RegisterGraphQl(ContainerBuilder builder)
        {
            var schemaFabric = StartenumSchemaFabric.Create();
            var schema = schemaFabric
                .Register(new ListsSchemaDefiner())
                .Register(new SocialSchemaDefiner())
                .Done();

            builder.RegisterInstance(schema).As<ISchema>().SingleInstance();
            builder.RegisterType<StartenumEntitiesSqlServerFactory>().As<StartenumEntitiesFactory>();
            builder.RegisterType<EntitiesSettings>().As<IEntitiesSettings>();
            builder.RegisterType<CorsConfiguration>().As<ICorsSettings>();
            builder.RegisterType<AuthSettings>().As<IAuthSettings>();
            builder.RegisterType<FilesSettings>().As<IFilesSettings>();

            builder.RegisterInstance(new DocumentExecuter()).As<IDocumentExecuter>().SingleInstance();
            builder.RegisterInstance(new DocumentWriter(true)).As<IDocumentWriter>().SingleInstance();
        }
    }
}