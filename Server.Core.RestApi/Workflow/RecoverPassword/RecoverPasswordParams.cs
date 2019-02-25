using Server.Core.Common.Entities.Users;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.RestApi.Workflow.RecoverPassword
{
    /// <summary>
    /// Параметры для шага восстановления пароля.
    /// </summary>
    public class RecoverPasswordParams:StepParameters
    {
        /// <summary>
        /// Пользователь у которого восстанавливается пароль.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Новый пароль.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Ip вызова с клиента.
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// Ответ по смене пароля.
        /// </summary>
        public MessageOutputBase Response { get; set; }

        /// <summary>
        /// Номер для проверки.
        /// </summary>
        public string Number { get; set; }
    }
}