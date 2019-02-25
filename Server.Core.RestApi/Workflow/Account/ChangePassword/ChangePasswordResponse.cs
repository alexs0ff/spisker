using Server.Core.Common.Messages;

namespace Server.Core.RestApi.Workflow.Account.ChangePassword
{
    /// <summary>
    /// Сообщение об успешном смене пароля.
    /// </summary>
    public class ChangePasswordResponse:MessageOutputBase
    {
        /// <summary>
        /// Имя пользователя у которого сменили пароль.
        /// </summary>
        public string UserName { get; set; }
    }
}