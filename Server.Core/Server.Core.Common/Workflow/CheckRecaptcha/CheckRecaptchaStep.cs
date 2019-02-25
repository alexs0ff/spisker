using System.Threading.Tasks;
using Server.Core.Common.Recaptcha;

namespace Server.Core.Common.Workflow.CheckRecaptcha
{
    /// <summary>
    /// Шаг проверки рекапчи.
    /// </summary>
    public class CheckRecaptchaStep : StepBase<CheckRecaptchaParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(CheckRecaptchaParams state)
        {
            var service = StartEnumServer.Instance.GetService<IRecaptchaService>();

            var result = await service.Validate(state.RecaptchaToken);

            if (!result)
            {
                state.Response = new InvalidRecaptchaResponse();

                return Finish();
            }

            return Success();
        }
    }
}
