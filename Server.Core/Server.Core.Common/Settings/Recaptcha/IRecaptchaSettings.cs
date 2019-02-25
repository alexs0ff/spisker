namespace Server.Core.Common.Settings.Recaptcha
{
    /// <summary>
    /// Интерфейс к настройкам рекапчи.
    /// </summary>
    public interface IRecaptchaSettings: ISettings
    {
        /// <summary>
        /// Секретный ключ google.
        /// </summary>
        string SecretKey { get; }
    }
}
