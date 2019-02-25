using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.FindUsers
{
    /// <summary>
    /// Параметры шага поиска пользователей.
    /// </summary>
    public class FindUsersParams:StepParameters
    {
        /// <summary>
        /// Имя пользователя кто ищет.
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Строка поиска.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ответ.
        /// </summary>
        public FindUsersResponse Response { get; set; }
    }
}
