using System.Threading.Tasks;
using Server.Core.Common.Workflow;
using Server.Core.Users.Models;

namespace Server.Core.Users.Workflow.GetAccountSettings
{
    /// <summary>
    /// Шаг получения параметров пользователя.
    /// </summary>
    public class GetAccountSettingsStep:StepBase<GetAccountSettingsParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(GetAccountSettingsParams state)
        {
            await Task.Yield();

            state.Response = new GetAccountSettingsResponse();
            state.Response.Settings = new AccountSettingsModel();
            state.Response.Settings.Email = state.User.Email;
            state.Response.Settings.FirstName = state.User.FirstName;
            state.Response.Settings.LastName = state.User.LastName;
            state.Response.Settings.MiddleName = state.User.MiddleName;

            return Success();
        }
    }
}
