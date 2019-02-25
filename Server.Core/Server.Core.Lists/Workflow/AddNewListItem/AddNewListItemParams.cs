using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.AddNewListItem
{
    /// <summary>
    /// Параметры добавления пункта списка.
    /// </summary>
    public class AddNewListItemParams: StepParameters
    {
        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Содержание нового списка.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Код пункта списка после которого необходимо добавить пункт.
        /// </summary>
        public string AfterListItemId { get; set; }

        /// <summary>
        /// Ответ.
        /// </summary>
        public AddNewListItemResponse Response { get; set; }
    }
}
