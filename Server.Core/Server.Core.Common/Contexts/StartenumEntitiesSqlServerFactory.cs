using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Server.Core.Common.Repositories;
using Microsoft.Extensions.Configuration;
using Server.Core.Common.Settings.Entities;

namespace Server.Core.Common.Contexts
{
    /// <summary>
    /// Фабрика для контекста базы.
    /// </summary>
    public class StartenumEntitiesSqlServerFactory: StartenumEntitiesFactory
    {
        /// <summary>
        /// Создание контекста.
        /// </summary>
        /// <returns>Созданный контекст.</returns>
        public override StartenumEntities Create()
        {
            var settings = StartEnumServer.Instance.GetSettings<IEntitiesSettings>();

            var builder = new DbContextOptionsBuilder<StartenumEntities>();

            builder.UseSqlServer(settings.ConnectionString);

            return new StartenumEntities(builder.Options);
        }
    }
}
