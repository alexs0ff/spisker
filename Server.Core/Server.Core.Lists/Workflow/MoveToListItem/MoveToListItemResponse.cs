using System;
using Server.Core.Common.Messages;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.MoveToListItem
{
    /// <summary>
    /// Ответ по перемещению пункта списка.
    /// </summary>
    public class MoveToListItemResponse:MessageOutputBase
    {
        /// <summary>
        /// Пункт скопированного списка. 
        /// </summary>
        public ListItemModel ListItem { get; set; }

        /// <summary>
        /// Код списка который необходимо было переместить.
        /// </summary>
        public Guid ListItemId { get; set; }
    }
}
