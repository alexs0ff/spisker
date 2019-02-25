using Server.Core.Common.Messages;

namespace Server.Core.RestApi.Workflow.Account.ChangePassword
{
    /// <summary>
    /// Запрос на смену пароля.
    /// </summary>
    public class ChangePasswordRequest:MessageInputBase
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
    }
}