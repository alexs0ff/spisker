namespace Server.Core.Common.Settings.Mail
{
    /// <summary>
    /// Интерфейс настроек для службы email.
    /// </summary>
    public interface IMailingServiceSettings: ISettings
    {
        /// <summary>
        /// Хост mail сервера.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Порт для хоста mail сервера.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Пароль для mail сервера.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Логин для mail сервера.
        /// </summary>
        string Login { get; }

        /// <summary>
        /// Признак того, что необходимо использовать SSL соединение.
        /// </summary>
        bool UseSsl { get; }

        /// <summary>
        /// Получает email адрес на который отправляются информационные письма.
        /// </summary>
        string InfoEmail { get; }

        /// <summary>
        /// Получает email адрес на который отправляются отзывы.
        /// </summary>
        string FeedbackEmail { get; }
    }
}
