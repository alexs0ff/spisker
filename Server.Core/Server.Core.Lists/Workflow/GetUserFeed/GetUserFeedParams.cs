using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.GetUserFeed
{
    /// <summary>
    /// Параметры для шага получения ленты пользователя.
    /// </summary>
    public class GetUserFeedParams: StepParameters
    {
        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Последний идентификатор списка.
        /// </summary>
        public string LastListId { get; set; }

        /// <summary>
        /// Ответ на запросы по ленте пользователя.
        /// </summary>
        public GetUserFeedResponse ListsResponse { get; set; }
    }
}
