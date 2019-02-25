using Server.Core.Common.Messages;
using Server.Core.Users.Models;

namespace Server.Core.Users.Workflow.GetAccountSettings
{
    /// <summary>
    /// Ответ с параметрами для пользователя.
    /// </summary>
    public class GetAccountSettingsResponse:MessageOutputBase
    {
        public AccountSettingsModel Settings { get; set; }
    }
}
