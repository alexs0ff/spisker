using Server.Core.Common.Messages;
using Server.Core.Social.Models;

namespace Server.Core.Social.Workflow.GetProfile
{
    /// <summary>
    /// Ответ с профилем пользователя.
    /// </summary>
    public class GetProfileResponse:MessageOutputBase
    {
        /// <summary>
        /// Профиль пользователя.
        /// </summary>
        public PortalUserProfileModel Profile { get; set; }
    }
}
