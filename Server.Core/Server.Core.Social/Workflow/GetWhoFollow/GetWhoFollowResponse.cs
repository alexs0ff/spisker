using System;
using System.Collections.Generic;
using Server.Core.Common.Messages;
using Server.Core.Social.Models;

namespace Server.Core.Social.Workflow.GetWhoFollow
{
    /// <summary>
    /// Ответ со списком следователей.
    /// </summary>
    public class GetWhoFollowResponse:MessageOutputBase
    {
        /// <summary>
        /// Список найденных следователей.
        /// </summary>
        public List<PortalUserProfileModel> Profiles { get; set; }

        /// <summary>
        /// Код последнего фолловера.
        /// </summary>
        public Guid? LastFollowerId { get; set; }

        /// <summary>
        /// Имя пользователя на которого распространяются сведения о подписке.
        /// </summary>
        public string ForUser { get; set; }
    }
}
