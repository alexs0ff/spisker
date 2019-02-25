using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.RestApi.Workflow.RecoverPassword
{
    /// <summary>
    /// Запрос на восстановление пароля.
    /// </summary>
    public class RecoverPasswordRequest:MessageInputBase, IUserName
    {
        /// <summary>
        /// Токен для рекапчи.
        /// </summary>
        public string RecaptchaToken { get; set; }

        /// <summary>
        /// Новый пароль.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Номер для проверки.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Имя пользователя у которого восстанавливается пароль.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Ip вызова с клиента.
        /// </summary>
        public string ClientIp { get; set; }
    }
}