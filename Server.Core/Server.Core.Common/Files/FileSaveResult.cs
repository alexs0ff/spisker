using System;

namespace Server.Core.Common.Files
{
    /// <summary>
    /// Результат выполнения сохранения файла.
    /// </summary>
    public class FileSaveResult
    {
        /// <summary>
        /// Полный путь к файлу из вне.
        /// </summary>
        public string FullUrl { get; set; }

        /// <summary>
        /// Код файла.
        /// </summary>
        public Guid FileId { get; set; }
    }
}
