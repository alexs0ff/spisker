using System;
using System.Text;
using NLog;
using Server.Core.Common.Logger;

namespace Server.Core.RestApi.Infrastructure.Logger
{

    /// <summary>
    /// Реализация логгера.
    /// </summary>
    public class Logger: IServerLogger
    {
        private readonly NLog.Logger _logger;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Logger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Логирование информации.
        /// </summary>
        /// <param name="system">Система.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Параметры.</param>
        public void LogInfo(Type system,string message, params LoggerParameter[] parameters)
        {
            Log(system, null, message, LogLevel.Info, parameters);
        }

        /// <summary>
        /// Логирование ошибки.
        /// </summary>
        /// <param name="system">Система.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Параметры.</param>
        public void LogError(Type system, string message, params LoggerParameter[] parameters)
        {
            Log(system, null, message, LogLevel.Error, parameters);
        }

        /// <summary>
        /// Логирование информации.
        /// </summary>
        /// <typeparam name="TSystem">Система.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Параметры.</param>
        public void LogInfo<TSystem>(string message, params LoggerParameter[] parameters)
        {
            Log(typeof(TSystem), null, message, LogLevel.Info, parameters);
        }

        /// <summary>
        /// Логирование ошибки.
        /// </summary>
        /// <typeparam name="TSystem">Система.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Параметры.</param>
        public void LogError<TSystem>(string message, params LoggerParameter[] parameters)
        {
            Log(typeof(TSystem), null, message, LogLevel.Error, parameters);
        }

        /// <summary>
        /// Логирование ошибки.
        /// </summary>
        /// <typeparam name="TSystem">Система.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="ex">Исключение.</param>
        /// <param name="parameters">Параметры.</param>
        public void LogError<TSystem>(Exception ex, string message, params LoggerParameter[] parameters)
        {
            Log(typeof(TSystem),ex, message, LogLevel.Error, parameters);
        }

        /// <summary>
        /// Логирование исключения.
        /// </summary>
        /// <param name="system">Система.</param>
        /// <param name="level">Уровень.</param>
        /// <param name="ex">Исключение.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="parameters">Доп параметры.</param>
        private void Log(Type system, Exception ex, string message, LogLevel level,params LoggerParameter[] parameters)
        {
            
            var logEntry = new LogEventInfo(level, string.Empty, message);
            if (ex != null)
            {

                Exception e = ex;
                var exText = new StringBuilder();
                while (e != null)
                {
                    exText.AppendLine(e.GetType().FullName);
                    exText.AppendLine();
                    exText.AppendLine(e.Message);
                    exText.AppendLine();
                    exText.AppendLine(e.StackTrace);
                    exText.AppendLine();

                    e = e.InnerException;
                }

                logEntry.Properties["Exception"] = ReplaceNewLines(exText.ToString());
            }
            
            WriteParameters(system,parameters, logEntry);

            _logger.Log(logEntry);
        }

        private static void WriteParameters(Type system,LoggerParameter[] parameters, LogEventInfo logEntry)
        {
            logEntry.Properties["ServerSystem"] = system.Name;
            //${event-properties:item=MyValue} -- renders "My custom string"
            if (parameters != null)
            {
                foreach (var par in parameters)
                {
                    logEntry.Properties[par.Name] = par.Value;
                }
            }
        }

        private string ReplaceNewLines(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            return value.Replace(Environment.NewLine, " ");
        }
    }
}