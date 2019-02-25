using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.GetUserFeed
{
    /// <summary>
    /// Запрос для получения ленты пользователя.
    /// </summary>
    public class GetUserFeedRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Последний идентификатор.
        /// </summary>
        public string LastListId { get; set; }
    }
}
