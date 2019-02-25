using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.RemoveList
{
    /// <summary>
    /// Параметры для операции удаления списка.
    /// </summary>
    public class RemoveListParams: StepParameters
    {
        /// <summary>
        /// Ответ на добавление нового списка.
        /// </summary>
        public RemoveListResponse Response { get; set; }

        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

    }
}
