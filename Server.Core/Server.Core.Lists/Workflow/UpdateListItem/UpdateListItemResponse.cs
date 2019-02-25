using Server.Core.Common.Messages;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.UpdateListItem
{
    /// <summary>
    /// Ответ по обновляемому списку.
    /// </summary>
    public class UpdateListItemResponse:MessageOutputBase
    {
        /// <summary>
        /// Обновленный пункт списка.
        /// </summary>
        public ListItemModel ListItem { get; set; }
    }
}
