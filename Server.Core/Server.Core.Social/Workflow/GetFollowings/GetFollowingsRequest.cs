using System;
using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.GetFollowings
{
    /// <summary>
    /// Запрос на получение подписок пользователя.
    /// </summary>
    public class GetFollowingsRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя по которому необходимо получить подписки.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Код последней подписки.
        /// </summary>
        public Guid? LastFollowingId { get; set; }

        /// <summary>
        /// Строка поиска.
        /// </summary>
        public string Search { get; set; }
    }
}
