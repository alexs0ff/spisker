using Server.Core.Common.Messages;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.UpdateList
{
    /// <summary>
    /// Ответ на запрос обновления списка.
    /// </summary>
    public class UpdateListResponse:MessageOutputBase
    {
        /// <summary>
        /// Обновленная модель.
        /// </summary>
        public ListModel List { get; set; }
    }
}
