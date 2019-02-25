using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.UpdateList
{
    /// <summary>
    /// Праметры обновления списка.
    /// </summary>
    public class UpdateListParams: StepParameters
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Название списка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ответ на обновление списка.
        /// </summary>
        public UpdateListResponse Response { get; set; }
    }
}
