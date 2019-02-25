using Server.Core.Common.Messages;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.AddNewListItem
{
    /// <summary>
    /// Ответ на добавление пункта списка.
    /// </summary>
    public class AddNewListItemResponse:MessageOutputBase
    {
        /// <summary>
        /// Добавленнный список.
        /// </summary>
        public ListItemModel ListItem { get; set; }
    }
}
