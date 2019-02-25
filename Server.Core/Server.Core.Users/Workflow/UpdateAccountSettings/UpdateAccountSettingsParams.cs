using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;
using Server.Core.Users.Models;

namespace Server.Core.Users.Workflow.UpdateAccountSettings
{
    /// <summary>
    /// Параметры для шага обновления данных по пользователю.
    /// </summary>
    public class UpdateAccountSettingsParams: StepParameters
    {
        /// <summary>
        /// Обновляемые настройки.
        /// </summary>
        public AccountSettingsModel Settings { get; set; }

        /// <summary>
        /// Пользователь по которому собираются параметры.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Ответ на запрос.
        /// </summary>
        public UpdateAccountSettingsResponse Response { get; set; }
    }
}
