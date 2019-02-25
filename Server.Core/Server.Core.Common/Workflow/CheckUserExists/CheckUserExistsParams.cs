using Server.Core.Common.Entities.Users;
using Server.Core.Common.Messages;

namespace Server.Core.Common.Workflow.CheckUserExists
{
    /// <summary>
    /// Запрос на получение списка для конкретного пользователя.
    /// </summary>
    public class CheckUserExistsParams: StepParameters
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

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
