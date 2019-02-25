namespace Server.Core.Common.Settings.Files
{
    /// <summary>
    /// Настройки файлового модуля.
    /// </summary>
    public interface IFilesSettings:ISettings
    {
        /// <summary>
        /// Получает первый сетевой путь к сохраняемым файлам.
        /// </summary>
        string ServerPath1 { get; }

        /// <summary>
        /// Получает первый локальный путь к сохраняемым файлам.
        /// </summary>
        string LocalPath1 { get; }

        /// <summary>
        /// Получает первый локальный путь к сохраняемым файлам.
        /// </summary>
        long MaxSize { get; }
    }
}
