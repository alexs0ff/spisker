using Server.Core.Common.Messages;

namespace Server.Core.Users.Workflow.UpdateAccountSettings
{
    /// <summary>
    /// Ответ на обновление данных пользователя.
    /// </summary>
    public class UpdateAccountSettingsResponse:MessageOutputBase
    {
        /// <summary>
        /// Имя пользователя по которому обновились параметры.
        /// </summary>
        public string UserName { get; set; }
    }
}
