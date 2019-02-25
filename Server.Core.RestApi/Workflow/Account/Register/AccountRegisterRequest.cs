using Server.Core.Common.Messages;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    /// <summary>
    /// Запрос о регистрации пользователя.
    /// </summary>
    public class AccountRegisterRequest:MessageInputBase
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Ip регистрации.
        /// </summary>
        public string Ip { get; set; }
    }
}