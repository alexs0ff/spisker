using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.RestApi.Workflow.Account.ChangePassword
{
    /// <summary>
    /// Параметры шага смене пароля.
    /// </summary>
    public class ChangePasswordParams:StepParameters
    {
        /// <summary>
        /// Имя пользователя у которого необходимо сменить пароль.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Старый пароль.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Новый пароль.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Ответ по пользователю.
        /// </summary>
        public MessageOutputBase Response { get; set; }
    }
}