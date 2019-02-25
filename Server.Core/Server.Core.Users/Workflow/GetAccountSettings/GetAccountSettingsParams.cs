using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Users.Workflow.GetAccountSettings
{
    /// <summary>
    /// Параметры для шага получения данных по пользователю для редактирования.
    /// </summary>
    public class GetAccountSettingsParams: StepParameters
    {
        /// <summary>
        /// Пользователь по которому собираются параметры.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Ответ с параметрами.
        /// </summary>
        public GetAccountSettingsResponse Response { get; set; }
    }
}
