using System;
using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.StopFollowing
{
    /// <summary>
    /// Ответ от метода по отписке пользователя.
    /// </summary>
    public class StopFollowingResponse: MessageOutputBase
    {
        /// <summary>
        /// Код пользователя от которого отписываются.
        /// </summary>
        public Guid ToUserId { get; set; }

        /// <summary>
        /// Код отписавшегося пользователя.
        /// </summary>
        public Guid SubscriberUserId { get; set; }
    }
}
