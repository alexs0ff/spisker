using System.Threading.Tasks;

namespace Server.Core.Common.Workflow.CheckUserExists
{
    /// <summary>
    /// Шаг при котором пользователь не найден.
    /// </summary>
    public class UserNotFoundStep : StepBase<UserNotFoundParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Состояние перехода. если состояние не меняется, тогда исполнение прекращается.</returns>
        public override async Task<StepResult> Execute(UserNotFoundParams state)
        {
            await Task.Yield();
            state.Response = new UserNotFoundResponse();

            return Success();
        }
    }
}
