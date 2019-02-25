using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Server.Core.Common.Settings.API;

namespace Server.Core.RestApi.Infrastructure.Settings
{
    public class CorsConfiguration: ICorsSettings
    {
        private readonly IConfiguration _configuration;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public CorsConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Получает сетевой путь для включения в Cors заголовок.
        /// </summary>
        public string ClientUrl => _configuration["Cors:ClientUrl"];
    }
}
