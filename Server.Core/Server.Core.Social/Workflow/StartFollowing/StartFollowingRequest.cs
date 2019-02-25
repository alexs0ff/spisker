using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.StartFollowing
{
    /// <summary>
    /// Запрос за подпиской на пользователя.
    /// </summary>
    public class StartFollowingRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя пользователя за которым идет следование.
        /// </summary>
        public string ToUserName { get; set; }
    }
}
