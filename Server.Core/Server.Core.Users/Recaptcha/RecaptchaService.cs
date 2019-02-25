using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Recaptcha;
using Server.Core.Common.Settings.Recaptcha;

namespace Server.Core.Users.Recaptcha
{
    /// <summary>
    /// Реализация сервиса рекапчи.
    /// </summary>
    public class RecaptchaService: IRecaptchaService
    {
        /// <summary>
        /// Производит проверку введеной гугловской капчи.
        /// </summary>
        /// <param name="token">Гугловский токен.</param>
        /// <returns>Признак успеха.</returns>
        public async Task<bool> Validate(string token)
        {
            var client = new System.Net.WebClient();

            string privateKey = StartEnumServer.Instance.GetSettings<IRecaptchaSettings>().SecretKey;

            var reply = await client.DownloadStringTaskAsync($"https://www.google.com/recaptcha/api/siteverify?secret={privateKey}&response={token}");

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RecaptchaResponse>(reply);

            return captchaResponse.Success;
        }
    }
}
