using Server.Core.Common.Messages;

namespace Server.Core.Users.Workflow.GetAccountSettings
{
    /// <summary>
    /// Запрос на получение параметров пользователя. 
    /// </summary>
    public class GetAccountSettingsRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя по которому собираются параметры.
        /// </summary>
        public string UserName { get; set; }
    }
}
