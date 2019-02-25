using Server.Core.Lists.Workflow.Messages;

namespace Server.Core.Lists.Workflow.GetUserFeed
{
    /// <summary>
    /// Ответ с лентой запроса.
    /// </summary>
    public class GetUserFeedResponse: FeedResponseBase
    {
        public GetUserFeedResponse()
        {
            FeedKind = FeedKindConstants.UserFeed;
        }

        /// <summary>
        /// Имя пользователя для выборки ленты.
        /// </summary>
        public string UserName { get; set; }
    }
}
