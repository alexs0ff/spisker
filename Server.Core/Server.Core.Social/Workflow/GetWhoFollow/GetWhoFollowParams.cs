using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.GetWhoFollow
{
    public class GetWhoFollowParams:StepParameters
    {
        /// <summary>
        /// ПОльзователь у которого необходимо получить следователей.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Код последнего подписчика.
        /// </summary>
        public Guid? LastFollowerId { get; set; }

        /// <summary>
        /// Строка поиска.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Ответ с последователями.
        /// </summary>
        public GetWhoFollowResponse Response { get; set; }

    }
}
