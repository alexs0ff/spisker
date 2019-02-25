using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Users.Workflow.UpdateAccountAvatar
{
    /// <summary>
    /// Параметры шага обновления аватара.
    /// </summary>
    public class UpdateAccountAvatarParams:StepParameters
    {
        /// <summary>
        /// Ip клиента.
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// Пользователь по которому изменяется аватар.
        /// </summary>
        public PortalUser User { get; set; }
        
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Данные файла.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Ответ по обновлению аватара.
        /// </summary>
        public UpdateAccountAvatarResponse Response { get; set; }

        /// <summary>
        /// Базовый путь к файлам
        /// </summary>
        public string RootPath { get; set; }
    }
}
