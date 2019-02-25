using Autofac;
using Server.Core.Common.Files;
using Server.Core.Common.Repositories.Files;
using Server.Core.Common.Settings.Files;
using Server.Core.Files.Repositories;

namespace Server.Core.Files
{
    /// <summary>
    /// DI модуль для регистраций сервисов для работы с файлами.
    /// </summary>
    public class FilesDiModule: Module
    {
        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StoredFileRepository>().As<IStoredFileRepository>();
            builder.RegisterType<FileStoreService>().As<IFileStoreService>();
            //builder.RegisterType<FilesConfiguration>().As<IFilesSettings>().SingleInstance();
        }
    }
}
