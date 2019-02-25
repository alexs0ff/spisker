using System;
using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.RemoveListItem
{
    /// <summary>
    /// Ответ на удаляемый список.
    /// </summary>
    public class RemoveListItemResponse: MessageOutputBase
    {
        /// <summary>
        /// Код удаленного списка.
        /// </summary>
        public Guid ListItemId { get; set; }
    }
}
