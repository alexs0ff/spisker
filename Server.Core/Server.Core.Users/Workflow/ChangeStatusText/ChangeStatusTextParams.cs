using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Users.Workflow.ChangeStatusText
{
    /// <summary>
    /// Параметры шага по изменению статуса.
    /// </summary>
    public class ChangeStatusTextParams:StepParameters
    {
        /// <summary>
        /// Пользователь у которого изменяется статус.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Устанавливаемый статус.
        /// </summary>
        public string NewStatus { get; set; }

        /// <summary>
        /// Ответ по установке статуса.
        /// </summary>
        public ChangeStatusTextResponse Response { get; set; }
    }
}
