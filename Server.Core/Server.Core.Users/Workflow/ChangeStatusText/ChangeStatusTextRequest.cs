using Server.Core.Common.Messages;

namespace Server.Core.Users.Workflow.ChangeStatusText
{
    /// <summary>
    /// Запрос по изменению статуса.
    /// </summary>
    public class ChangeStatusTextRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя у которого изменяется статус.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Устанавливаемый статус.
        /// </summary>
        public string NewStatus { get; set; }
    }
}
