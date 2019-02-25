using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.UpdateListItemCheck
{
    /// <summary>
    /// Параметры для шага обновления состояния выбора пункта списка.
    /// </summary>
    public class UpdateListItemCheckParams: StepParameters
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
        /// Состояние пункта списка.
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Ответ на обновление пункта списка.
        /// </summary>
        public UpdateListItemCheckResponse Response { get; set; }
    }
}
