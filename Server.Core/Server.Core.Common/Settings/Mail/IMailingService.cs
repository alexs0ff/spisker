using System.Collections.Generic;
using Server.Core.Common.Services;

namespace Server.Core.Common.Settings.Mail
{
    /// <summary>
    /// Интерфейс службы отправки email.
    /// TODO перенести в другой NS.
    /// </summary>
    public interface IMailingService: IServiceBase
    {
        /// <summary>
        /// Отправлет сообщение на указаный адрес.
        /// </summary>
        /// <param name="recipient">Получатель или получатели.</param>
        /// <param name="title">Заголовок.</param>
        /// <param name="body">Тело сообщения.</param>
        /// <param name="attachments">Список вложений.</param>
        void Send(string recipient, string title, string body, IList<KeyValuePair<string, byte[]>> attachments);

        /// <summary>
        /// Отправлет сообщение на указаный адрес.
        /// </summary>
        /// <param name="recipient">Получатель или получатели.</param>
        /// <param name="title">Заголовок.</param>
        /// <param name="body">Тело сообщения.</param>
        void Send(string recipient, string title, string body);
    }
}
