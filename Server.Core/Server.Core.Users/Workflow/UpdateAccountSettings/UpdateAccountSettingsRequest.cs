using Server.Core.Common.Messages;
using Server.Core.Users.Models;

namespace Server.Core.Users.Workflow.UpdateAccountSettings
{
    /// <summary>
    /// Запрос на обновление данных по пользователю.
    /// </summary>
    public class UpdateAccountSettingsRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя по которому обновляются параметры.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Обновляемые настройки.
        /// </summary>
        public AccountSettingsModel Settings { get; set; }
    }
}
