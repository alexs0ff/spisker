using Server.Core.Common.Entities.Users;

namespace Server.Core.Common.Workflow.CheckUserListExists
{
    /// <summary>
    /// Параметры не нахождения списка пользователя.
    /// </summary>
    public class UserListNotFoundParams:StepParameters
    {
        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Код пользовательского списка, который не нашли.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Ответ то что пользовательский список не найден.
        /// </summary>
        public UserListNotFoundResponse Response { get; set; }
    }
}
