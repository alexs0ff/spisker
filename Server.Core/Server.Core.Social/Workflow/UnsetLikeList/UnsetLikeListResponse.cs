using System;
using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.UnsetLikeList
{
    /// <summary>
    /// Ответ на отзыв лайка.
    /// </summary>
    public class UnsetLikeListResponse:MessageOutputBase
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Текущее количество лайков у списка.
        /// </summary>
        public long LikeCount { get; set; }
    }
}
