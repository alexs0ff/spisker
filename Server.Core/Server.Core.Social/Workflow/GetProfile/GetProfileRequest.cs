using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.GetProfile
{
    /// <summary>
    /// Запрос на получение профиля пользователя.
    /// </summary>
    public class GetProfileRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя по которому необходимо получить профиль.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public string CurrentUserName { get; set; }
    }
}
