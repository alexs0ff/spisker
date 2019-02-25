using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.RestApi.Workflow.Account.StartRecoverPassword
{
    /// <summary>
    /// Запрос на восстановление пароля.
    /// </summary>
    public class StartRecoverPasswordRequest:MessageInputBase, IUserName
    {
        /// <summary>
        /// Токен для рекапчи.
        /// </summary>
        public string RecaptchaToken { get; set; }

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