using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.StopFollowing
{
    /// <summary>
    /// Запрос на отписку от пользователя.
    /// </summary>
    public class StopFollowingRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя пользователя от которого отписываются.
        /// </summary>
        public string ToUserName { get; set; }
    }
}
