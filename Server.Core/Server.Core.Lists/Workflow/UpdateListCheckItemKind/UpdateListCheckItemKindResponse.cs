using System;
using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.UpdateListCheckItemKind
{
    /// <summary>
    /// Ответ по измененному типу выделения пунктов списка.
    /// </summary>
    public class UpdateListCheckItemKindResponse:MessageOutputBase
    {
        /// <summary>
        /// Код обновленного списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Обновленный тип выделения пунктов списка.
        /// </summary>
        public int ListCheckItemKind { get; set; }
    }
}
