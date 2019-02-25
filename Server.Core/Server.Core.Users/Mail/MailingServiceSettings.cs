using System.Globalization;
using Microsoft.Extensions.Configuration;
using Server.Core.Common.Settings.Mail;

namespace Server.Core.Users.Mail
{
    /// <summary>
    /// Реализация настроек для email службы.
    /// </summary>
    public class MailingServiceSettings : IMailingServiceSettings
    {
        private readonly IConfiguration _configuration;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public MailingServiceSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Хост mail сервера.
        /// </summary>
        public string Host => _configuration["MailingService:Host"];

        /// <summary>
        /// Порт для хоста mail сервера.
        /// </summary>
        public int Port => int.Parse(_configuration["MailingService:Port"],CultureInfo.InvariantCulture);

        /// <summary>
        /// Пароль для mail сервера.
        /// </summary>
        public string Password => _configuration["MailingService:Password"];

        /// <summary>
        /// Логин для mail сервера.
        /// </summary>
        public string Login => _configuration["MailingService:Login"];

        /// <summary>
        /// Признак того, что необходимо использовать SSL соединение.
        /// </summary>
        public bool UseSsl => bool.Parse(_configuration["MailingService:UseSsl"]);

        /// <summary>
        /// Получает email адрес на который отправляются информационные письма.
        /// </summary>
        public string InfoEmail => _configuration["MailingService:InfoEmail"];

        /// <summary>
        /// Получает email адрес на который отправляются отзывы.
        /// </summary>
        public string FeedbackEmail => _configuration["MailingService:FeedbackEmail"];
    }
}
