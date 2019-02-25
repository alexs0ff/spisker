using System;
using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.RepostList
{
    /// <summary>
    /// Ответ на репост списка.
    /// </summary>
    public class RepostListResponse:MessageOutputBase
    {
        /// <summary>
        /// Код списка который создался у пользователя.
        /// </summary>
        public Guid RepostedListId { get; set; }

        /// <summary>
        /// Код оригинального списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Текущее количество репостов у списка.
        /// </summary>
        public long RepostCount { get; set; }
    }
}
