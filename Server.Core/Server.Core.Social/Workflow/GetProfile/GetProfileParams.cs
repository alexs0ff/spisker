using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.GetProfile
{
    /// <summary>
    /// Параметры для шага по получению профиля.
    /// </summary>
    public class GetProfileParams: StepParameters
    {
        /// <summary>
        /// ПОльзователь у которого берется профиль.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Ответ с профилем пользователя.
        /// </summary>
        public GetProfileResponse Response { get; set; }

    }
}
