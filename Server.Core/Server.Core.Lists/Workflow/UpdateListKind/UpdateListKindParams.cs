using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.UpdateListKind
{
    /// <summary>
    /// Параметры шага смены типа спика.
    /// </summary>
    public class UpdateListKindParams:StepParameters
    {
        /// <summary>
        /// Новый код типа списка.
        /// </summary>
        public int ListKindId { get; set; }

        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Ответ по обновлению типа.
        /// </summary>
        public MessageOutputBase Response { get; set; }
    }
}
