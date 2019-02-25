using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.RestApi.Workflow.Account.StartRecoverPassword
{
    /// <summary>
    /// Параметры восстановления пароля.
    /// </summary>
    public class StartRecoverPasswordParams:StepParameters
    {
        /// <summary>
        /// Пользователь у которого восстанавливается пароль.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Ip вызова с клиента.
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// Ответ по смене пароля.
        /// </summary>
        public StartRecoverPasswordResponse Response { get; set; }
    }
}