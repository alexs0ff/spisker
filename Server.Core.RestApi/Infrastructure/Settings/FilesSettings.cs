using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Server.Core.Common.Settings.Files;

namespace Server.Core.RestApi.Infrastructure.Settings
{
    public class FilesSettings: IFilesSettings
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public FilesSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Получает первый сетевой путь к сохраняемым файлам.
        /// </summary>
        public string ServerPath1 => _configuration["Files:ServerPath1"];

        /// <summary>
        /// Получает первый локальный путь к сохраняемым файлам.
        /// </summary>
        public string LocalPath1 => _configuration["Files:LocalPath1"];

        /// <summary>
        /// Получает первый локальный путь к сохраняемым файлам.
        /// </summary>
        public long MaxSize => long.Parse(_configuration["Files:MaxSize"]);
    }
}
