using Server.Core.Common.Messages;

namespace Server.Core.Users.Workflow.UpdateAccountAvatar
{
    /// <summary>
    /// Запрос на обновление аватара.
    /// </summary>
    public class UpdateAccountAvatarRequest:MessageInputBase
    {
        /// <summary>
        /// Имя пользователя по которому обновляется аватар.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Ip клиента.
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// Данные файла.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Базовый путь к файлам
        /// </summary>
        public string RootPath { get; set; }
    }
}
