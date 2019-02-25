using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.SetLikeList
{
    /// <summary>
    /// Параметры шага установки лайка списка.
    /// </summary>
    public class SetLikeListParams: StepParameters
    {
        /// <summary>
        /// ПОльзователь который хочет поставить лайк.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Код списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Ответ на установку лайка.
        /// </summary>
        public SetLikeListResponse Response { get; set; }
    }
}
