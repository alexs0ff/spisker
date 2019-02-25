using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Users.Workflow.UpdateAccountSettings
{
    /// <summary>
    /// Шаг обновления настроек аккаунта.
    /// </summary>
    public class UpdateAccountSettingsStep:StepBase<UpdateAccountSettingsParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(UpdateAccountSettingsParams state)
        {
            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();

            await userRepository.UpdateMiddleName(state.User.PortalUserID, state.Settings.MiddleName);

            if (!string.IsNullOrWhiteSpace(state.Settings.FirstName))
            {
                await userRepository.UpdateFirstName(state.User.PortalUserID, state.Settings.FirstName);
            }

            if(!string.IsNullOrWhiteSpace(state.Settings.LastName))
            {
                await userRepository.UpdateLastName(state.User.PortalUserID, state.Settings.LastName);
            }

            if (!string.IsNullOrWhiteSpace(state.Settings.Email))
            {
                await userRepository.UpdateEmail(state.User.PortalUserID, state.Settings.Email);
            }

            state.Response = new UpdateAccountSettingsResponse
            {
                UserName = state.User.UserName
            };

            return Success();
        }
    }
}
