using System.Collections.Generic;

namespace Server.Core.Common.Messages
{
    /// <summary>
    /// Базовый класс для всех исходящих запросов.
    /// </summary>
    public class MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public MessageOutputBase()
        {
            Errors = new List<ErrorInfo>();
        }

        /// <summary>
        /// Список ошибок. 
        /// </summary>
        public List<ErrorInfo> Errors { get; set; }
    }
}
