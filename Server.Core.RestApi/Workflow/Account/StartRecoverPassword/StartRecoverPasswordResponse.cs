using Server.Core.Common.Messages;

namespace Server.Core.RestApi.Workflow.Account.StartRecoverPassword
{
    /// <summary>
    /// Ответ по восстановлению пароля.
    /// </summary>
    public class StartRecoverPasswordResponse:MessageOutputBase
    {
        /// <summary>
        /// Имя пользователя восстановившего пароль.
        /// </summary>
        public string UserName { get; set; }
    }
}