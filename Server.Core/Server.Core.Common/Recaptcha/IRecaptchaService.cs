using System.Threading.Tasks;
using Server.Core.Common.Services;

namespace Server.Core.Common.Recaptcha
{
    /// <summary>
    /// Сервис по рекапче.
    /// </summary>
    public interface IRecaptchaService:IServiceBase
    {
        /// <summary>
        /// Производит проверку введеной гугловской капчи.
        /// </summary>
        /// <param name="token">Гугловский токен.</param>
        /// <returns>Признак успеха.</returns>
        Task<bool> Validate(string token);
    }
}
