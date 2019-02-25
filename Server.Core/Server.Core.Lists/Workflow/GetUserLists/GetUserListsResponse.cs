using System;
using Server.Core.Lists.Workflow.Messages;

namespace Server.Core.Lists.Workflow.GetUserLists
{
    /// <summary>
    /// Ответ на запросы по пользовательским
    /// </summary>
    public class GetUserListsResponse: FeedResponseBase
    {
        public GetUserListsResponse()
        {
            FeedKind = FeedKindConstants.UserLists;
        }

        /// <summary>
        /// Получает код выбранного списка.
        /// </summary>
        public Guid? SelectedListId { get; set; }
    }
}
