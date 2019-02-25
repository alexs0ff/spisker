using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Logger;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.CheckListItemExists
{
    /// <summary>
    /// Шаг с ответом, что пункт списка не найден.
    /// </summary>
    public class ListItemNotFoundStep: StepBase<ListItemNotFoundParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(ListItemNotFoundParams state)
        {
            await Task.Yield();

            StartEnumServer.Instance.GetLogger()
                .LogError<ListItemNotFoundStep>($"Не найден пункт списка с кодом {state.ListItemId}",
                    new LoggerParameter("ListItemId", state.ListItemId));

            state.Response = new ListItemNotFoundResponse();

            return Success();
        }
    }
}
