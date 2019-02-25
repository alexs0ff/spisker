using Server.Core.Common.Messages;

namespace Server.Core.Users.Workflow.ChangeStatusText
{
    /// <summary>
    /// Ответ с измененным статусом.
    /// </summary>
    public class ChangeStatusTextResponse:MessageOutputBase
    {
        /// <summary>
        /// Имя пользователя у которого изменяется статус.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Новый статус для пользователя.
        /// </summary>
        public string NewStatus { get; set; }
    }
}
