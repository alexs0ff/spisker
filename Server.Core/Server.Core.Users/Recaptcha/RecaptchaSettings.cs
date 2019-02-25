using Microsoft.Extensions.Configuration;
using Server.Core.Common.Settings.Recaptcha;

namespace Server.Core.Users.Recaptcha
{
    /// <summary>
    /// Настройки для рекапчи.
    /// </summary>
    public class RecaptchaSettings : IRecaptchaSettings
    {
        private readonly IConfiguration _configuration;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public RecaptchaSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Секретный ключ google.
        /// </summary>
        public string SecretKey => _configuration["Recaptcha:SecretKey"];
    }
}
