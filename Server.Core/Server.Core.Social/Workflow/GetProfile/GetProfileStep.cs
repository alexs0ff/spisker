using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;
using Server.Core.Social.Workflow.Common;

namespace Server.Core.Social.Workflow.GetProfile
{
    /// <summary>
    /// Шаг получения профиля пользователя.
    /// </summary>
    public class GetProfileStep: ProfileStepBase<GetProfileParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(GetProfileParams state)
        {
            var profileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();
            
            var profile = await profileRepository.GetByUserId(state.User.PortalUserID);

            var model = await ProcessProfile(profile, state.User, state.CurrentUserName);

            state.Response = new GetProfileResponse
            {
                Profile = model
            };
            
            return Success();
        }
    }
}
