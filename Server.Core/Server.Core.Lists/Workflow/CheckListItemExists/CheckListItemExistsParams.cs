using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.CheckListItemExists
{
    /// <summary>
    /// Параметры существования пункта списка.
    /// </summary>
    public class CheckListItemExistsParams: StepParameters
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Код пункта списка.
        /// </summary>
        public string ListItemId { get; set; }

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
