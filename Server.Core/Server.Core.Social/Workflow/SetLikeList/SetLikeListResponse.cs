using System;
using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.SetLikeList
{
    /// <summary>
    /// Ответ от установки лайка списка.
    /// </summary>
    public class SetLikeListResponse:MessageOutputBase
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
