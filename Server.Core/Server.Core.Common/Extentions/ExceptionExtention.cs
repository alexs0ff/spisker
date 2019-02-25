using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Common.Extentions
{
    /// <summary>
    /// Методы расширения для Exception.
    /// </summary>
    public static class ExceptionExtention
    {
        /// <summary>
        /// Получает полную строковую детализацию исключения.
        /// </summary>
        /// <param name="exception">Исключение.</param>
        /// <returns>Строковая детализация.</returns>
        public static string GetExceptionDetails(this Exception exception)
        {
            var result = new StringBuilder();

            var ex = exception;

            while (ex != null)
            {
                result.Append($"{ex.GetType()}; {ex.Message}; {ex.StackTrace};");
                ex = ex.InnerException;

                if (ex != null)
                {
                    result.Append("-->");
                }

            }

            return result.ToString();
        }
    }
}
