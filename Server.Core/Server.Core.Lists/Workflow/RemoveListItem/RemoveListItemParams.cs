using System;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.RemoveListItem
{
    /// <summary>
    /// Параметры шага удаления пункта списка.
    /// </summary>
    public class RemoveListItemParams: StepParameters
    {
        /// <summary>
        /// Код пункта списка.
        /// </summary>
        public Guid ListItemId { get; set; }

        /// <summary>
        /// Ответ на удаление пункта списка.
        /// </summary>
        public RemoveListItemResponse Response { get; set; }
    }
}
