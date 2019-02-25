using System;
using System.Collections.Generic;
using Server.Core.Common.Messages;
using Server.Core.Social.Models;

namespace Server.Core.Social.Workflow.GetFollowings
{
    /// <summary>
    /// Ответ с подписками.
    /// </summary>
    public class GetFollowingsResponse:MessageOutputBase
    {
        /// <summary>
        /// Список найденных подписок.
        /// </summary>
        public List<PortalUserProfileModel> Profiles { get; set; }

        /// <summary>
        /// Код последней подписки.
        /// </summary>
        public Guid? LastFollowingId { get; set; }

        /// <summary>
        /// Имя пользователя на которого распространяются сведения о подписке.
        /// </summary>
        public string ForUser { get; set; }
    }
}
