using Server.Core.Common.Messages;

namespace Server.Core.Users.Workflow.UpdateAccountAvatar
{
    /// <summary>
    /// Ответ по обновлению аватара.
    /// </summary>
    public class UpdateAccountAvatarResponse:MessageOutputBase
    {
        /// <summary>
        /// Путь к аватару.
        /// </summary>
        public string ImageUrl { get; set; }


        /// <summary>
        /// Имя пользователя изменившего аватар.
        /// </summary>
        public string UserName { get; set; }
    }
}
