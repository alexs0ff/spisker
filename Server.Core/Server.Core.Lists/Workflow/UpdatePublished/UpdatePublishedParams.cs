using System;
using System.Collections.Generic;
using System.Text;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.UpdatePublished
{
    /// <summary>
    /// Параметры шага обновления признака публикации списка.
    /// </summary>
    public class UpdatePublishedParams: StepParameters
    {
        /// <summary>
        /// Признак публикации списка.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Ответ по обновлению списка.
        /// </summary>
        public MessageOutputBase Response { get; set; }
    }
}
