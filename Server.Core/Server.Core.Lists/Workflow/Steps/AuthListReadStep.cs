using System.Threading.Tasks;
using Server.Core.Common.Workflow;
using Server.Core.Lists.Workflow.Params;

namespace Server.Core.Lists.Workflow.Steps
{
    /// <summary>
    /// Шаг проверки прав на чтение определенного списка.
    /// </summary>
    public class AuthListReadStep: StepBase<AuthParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Состояние перехода. если состояние не меняется, тогда исполнение прекращается.</returns>
        public override async Task<StepResult> Execute(AuthParams state)
        {
            await Task.Yield();
            return Success();
        }
    }
}
