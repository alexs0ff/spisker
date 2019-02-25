using System.Threading.Tasks;
using Server.Core.Common.Logger;

namespace Server.Core.Common.Workflow.CheckUserListExists
{
    /// <summary>
    /// Шаг не нахождения пользовательского списка.
    /// </summary>
    public class UserListNotFoundStep:StepBase<UserListNotFoundParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(UserListNotFoundParams state)
        {
            await Task.Yield();

            StartEnumServer.Instance.GetLogger()
                .LogError<UserListNotFoundStep>($"Не найден пользовательский список с кодом {state.ListId}",
                    new LoggerParameter("ListId", state.ListId), new LoggerParameter("UserName", state.User.UserName));

            state.Response = new UserListNotFoundResponse();

            return Success();
        }
    }
}
