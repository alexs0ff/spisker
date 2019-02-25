using Server.Core.Common.Messages;

namespace Server.Core.Social.Workflow.FindUsers
{
    /// <summary>
    /// Запрос на поиск пользователей.
    /// </summary>
    public class FindUsersRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя кто ищет.
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Строка поиска.
        /// </summary>
        public string Name { get; set; }
    }
}
