using Server.Core.Common.Messages;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.AddNewList
{
    /// <summary>
    /// Ответ на добавление нового списа.
    /// </summary>
    public class AddNewListResponse: MessageOutputBase
    {
        /// <summary>
        /// Модель списка.
        /// </summary>
        public ListModel List { get; set; }
    }
}
