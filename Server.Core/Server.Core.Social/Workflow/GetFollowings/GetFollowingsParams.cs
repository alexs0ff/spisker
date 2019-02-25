using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.GetFollowings
{
    /// <summary>
    /// Параметры для шага по получени подписок.
    /// </summary>
    public class GetFollowingsParams:StepParameters
    {
        /// <summary>
        /// Пользователь у которого необходимо получить подписки.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Код последнего подписки.
        /// </summary>
        public Guid? LastFollowingId { get; set; }

        /// <summary>
        /// Строка поиска.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Ответ с подписками.
        /// </summary>
        public GetFollowingsResponse Response { get; set; }
    }
}
