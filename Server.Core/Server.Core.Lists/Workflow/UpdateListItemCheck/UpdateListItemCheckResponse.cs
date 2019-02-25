using Server.Core.Common.Messages;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.UpdateListItemCheck
{
    /// <summary>
    /// Ответ по обновляемуму состоянию пункта списка.
    /// </summary>
    public class UpdateListItemCheckResponse:MessageOutputBase
    {
        /// <summary>
        /// Обновленный пункт списка.
        /// </summary>
        public ListItemModel ListItem { get; set; }
    }
}
