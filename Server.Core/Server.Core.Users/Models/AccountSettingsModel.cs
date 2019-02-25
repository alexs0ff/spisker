namespace Server.Core.Users.Models
{
    /// <summary>
    /// Модель настроек акканта.
    /// </summary>
    public class AccountSettingsModel
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользоватея.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }
    }
}
