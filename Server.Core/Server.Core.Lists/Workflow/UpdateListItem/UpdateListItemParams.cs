using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.UpdateListItem
{
    /// <summary>
    /// Параметры для обновления списка.
    /// </summary>
    public class UpdateListItemParams: StepParameters
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Код пункта списка.
        /// </summary>
        public Guid ListItemId { get; set; }

        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Содержание пункта списка.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Ответ на обновление пункта списка.
        /// </summary>
        public UpdateListItemResponse Response { get; set; }
    }
}
