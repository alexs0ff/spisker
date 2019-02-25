using System;
using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.GetWhoFollow
{
    /// <summary>
    /// Запрос на получение следователей пользователя.
    /// </summary>
    public class GetWhoFollowRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя по которому необходимо получить следователей.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Код последнего подписчика.
        /// </summary>
        public Guid? LastFollowerId { get; set; }

        /// <summary>
        /// Строка поиска.
        /// </summary>
        public string Search { get; set; }
    }
}
