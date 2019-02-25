using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Common.Settings.Entities
{
    /// <summary>
    /// Интерфейс настроек для базы.
    /// </summary>
    public interface IEntitiesSettings:ISettings
    {
        /// <summary>
        /// Строка подключения к базе.
        /// </summary>
        string ConnectionString { get; }
    }
}
