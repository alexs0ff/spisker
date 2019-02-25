using System.Collections.Generic;
using Newtonsoft.Json;

namespace Server.Core.Users.Recaptcha
{
    /// <summary>
    /// Ответ от сервиса Recaptcha.
    /// </summary>
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
