namespace Server.Core.Common.Settings.API
{
    /// <summary>
    /// Настройки для Cors заголовков.
    /// </summary>
    public interface ICorsSettings: ISettings
    {
        /// <summary>
        /// Получает сетевой путь для включения в Cors заголовок.
        /// </summary>
        string ClientUrl { get; }
    }
}
