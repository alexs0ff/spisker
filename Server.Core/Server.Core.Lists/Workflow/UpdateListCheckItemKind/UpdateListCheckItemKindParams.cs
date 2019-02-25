using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.UpdateListCheckItemKind
{
    /// <summary>
    /// Параметры шага изменения типа выделения пунктов списка.
    /// </summary>
    public class UpdateListCheckItemKindParams:StepParameters
    {
        /// <summary>
        /// Обновляемый код типа выделения пунктов списка.
        /// </summary>
        public int ListCheckItemKindId { get; set; }

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
