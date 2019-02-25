using System.Threading.Tasks;
using Server.Core.Common.Logger;

namespace Server.Core.Common.Workflow.CheckListExists
{
    /// <summary>
    /// Шаг по не найденным спискам.
    /// </summary>
    public class ListNotFoundStep:StepBase<ListNotFoundParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Состояние перехода. если состояние не меняется, тогда исполнение прекращается.</returns>
        public override async Task<StepResult> Execute(ListNotFoundParams state)
        {
            await Task.Yield();

            StartEnumServer.Instance.GetLogger()
                .LogError<ListNotFoundStep>($"Не найден список с кодом {state.ListId}",
                    new LoggerParameter("ListId", state.ListId));

            state.Response = new ListNotFoundResponse();

            return Success();
        }
    }
}
