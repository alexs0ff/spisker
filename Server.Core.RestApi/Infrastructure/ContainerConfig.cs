using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Server.Core.Common;
using Server.Core.Files;
using Server.Core.Lists;
using Server.Core.Social;
using Server.Core.Users;

namespace Server.Core.RestApi.Infrastructure
{
    /// <summary>
    /// Конфигурация контейнера DI.
    /// </summary>
    public static class ContainerConfig
    {
        /// <summary>
        /// Инициализация DI.
        /// </summary>
        public static AutofacServiceProvider Init(IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            
            builder.Populate(services);
            builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();
            builder.RegisterModule(new ServerModule());
            builder.RegisterModule(new UsersDiModule());
            builder.RegisterModule(new ListsDiModule());
            builder.RegisterModule(new SocialDiModule());
            builder.RegisterModule(new FilesDiModule());

            builder.RegisterInstance(MapperConfig.Create());

            IContainer container = builder.Build();
            StartEnumServer.SetConfiguration(container);

            return new AutofacServiceProvider(container);
        }
        
    }
}