using Server.Core.Common.Entities.Users;
using Server.Core.Common.Messages;

namespace Server.Core.Common.Workflow.CheckUserListExists
{
    /// <summary>
    /// Параметры существования списка у пользователя.
    /// </summary>
    public class CheckUserListExistsParams: StepParameters
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Входящее сообщение.
        /// </summary>
        public MessageInputBase InputMessage { get; set; }
    }
}
