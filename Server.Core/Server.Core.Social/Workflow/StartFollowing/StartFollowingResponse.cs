using System;
using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.StartFollowing
{
    /// <summary>
    /// Ответ на подписку о пользователе.
    /// </summary>
    public class StartFollowingResponse:MessageOutputBase
    {
        /// <summary>
        /// Код пользователя за которым идет следование.
        /// </summary>
        public Guid ToUserId { get; set; }

        /// <summary>
        /// Код подписавшегося пользователя.
        /// </summary>
        public Guid SubscriberUserId { get; set; }
    }
}
