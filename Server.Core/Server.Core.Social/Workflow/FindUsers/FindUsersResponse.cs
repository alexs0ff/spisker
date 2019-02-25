using System.Collections.Generic;
using Server.Core.Common.Messages;
using Server.Core.Social.Models;

namespace Server.Core.Social.Workflow.FindUsers
{
    /// <summary>
    /// Ответ по пользователям.
    /// </summary>
    public class FindUsersResponse:MessageOutputBase
    {
        /// <summary>
        /// Список найденных пользователей.
        /// </summary>
        public List<PortalUserProfileModel> Profiles { get; set; }
    }
}
