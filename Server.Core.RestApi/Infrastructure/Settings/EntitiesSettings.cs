using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Server.Core.Common.Settings.Entities;

namespace Server.Core.RestApi.Infrastructure.Settings
{
    public class EntitiesSettings: IEntitiesSettings
    {
        private readonly IConfiguration _configuration;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public EntitiesSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Строка подключения к базе.
        /// </summary>
        public string ConnectionString => _configuration["ConnectionStrings:DefaultConnection"];
    }
}
