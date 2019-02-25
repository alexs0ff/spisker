using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.StopFollowing
{
    /// <summary>
    /// Параметры для шага отписки пользователя от другого пользователя.
    /// </summary>
    public class StopFollowingParams : StepParameters
    {
        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Имя пользователя за которым идет следование.
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// Ответ.
        /// </summary>
        public StopFollowingResponse Response { get; set; }
    }
}
