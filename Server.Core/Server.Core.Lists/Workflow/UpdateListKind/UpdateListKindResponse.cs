using System;
using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.UpdateListKind
{
    /// <summary>
    /// Ответ по обновлению типа списка.
    /// </summary>
    public class UpdateListKindResponse:MessageOutputBase
    {
        /// <summary>
        /// Код обновленного списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Обновленный тип списка.
        /// </summary>
        public int ListKind { get; set; }
    }
}
