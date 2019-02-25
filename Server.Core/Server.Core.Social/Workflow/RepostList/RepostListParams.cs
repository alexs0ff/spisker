using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.RepostList
{
    /// <summary>
    /// Параметры шага репоста.
    /// </summary>
    public class RepostListParams: StepParameters
    {
        /// <summary>
        /// ПОльзователь у которого берется профиль.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Ответ на установку лайка.
        /// </summary>
        public RepostListResponse Response { get; set; }
    }
}
