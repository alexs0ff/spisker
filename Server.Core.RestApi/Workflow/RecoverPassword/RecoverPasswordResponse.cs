using Server.Core.Common.Messages;

namespace Server.Core.RestApi.Workflow.RecoverPassword
{
    /// <summary>
    /// Сообщение о результате восстановления пароля.
    /// </summary>
    public class RecoverPasswordResponse:MessageOutputBase
    {
        /// <summary>
        /// Имя пользователя у которого восстановили пароль.
        /// </summary>
        public string UserName { get; set; }
    }
}