using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Common.Logger
{
    /// <summary>
    /// Логгер.
    /// </summary>
    public interface IServerLogger
    {
        /// <summary>
        /// Логирование информации.
        /// </summary>
        /// <typeparam name="TSystem">Система.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Параметры.</param>
        void LogInfo<TSystem>(string message, params LoggerParameter[] parameters);

        /// <summary>
        /// Логирование ошибки.
        /// </summary>
        /// <typeparam name="TSystem">Система.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Параметры.</param>
        void LogError<TSystem>(string message, params LoggerParameter[] parameters);

        /// <summary>
        /// Логирование ошибки.
        /// </summary>
        /// <typeparam name="TSystem">Система.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="ex">Исключение.</param>
        /// <param name="parameters">Параметры.</param>
        void LogError<TSystem>(Exception ex, string message, params LoggerParameter[] parameters);

        /// <summary>
        /// Логирование информации.
        /// </summary>
        /// <param name="system">Система.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Параметры.</param>
        void LogInfo(Type system,string message, params LoggerParameter[] parameters);

        /// <summary>
        /// Логирование ошибки.
        /// </summary>
        /// <param name="system">Система.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Параметры.</param>
        void LogError(Type system, string message, params LoggerParameter[] parameters);
    }
}
