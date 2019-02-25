using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;
using Server.Core.Social.Models;
using Server.Core.Social.Workflow.Common;

namespace Server.Core.Social.Workflow.FindUsers
{
    /// <summary>
    /// Шаг поиска пользователей.
    /// </summary>
    public class FindUsersStep: ProfileStepBase<FindUsersParams>
    {
        /// <summary>
        /// Минимальное значение символов для поиска человека.
        /// </summary>
        private const int MinCharacters = 3;

        private const int MaxCount = 8;

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(FindUsersParams state)
        {
            state.Response = new FindUsersResponse();
            state.Response.Profiles = new List<PortalUserProfileModel>();

            if (string.IsNullOrWhiteSpace(state.Name) || state.Name.Length < MinCharacters)
            {
                return Success();
            }

            var profileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();

            var users = StartEnumServer.Instance.GetRepository<IPortalUserRepository>().GetUsers(state.Name, MaxCount);

            foreach (var portalUser in users)
            {
                var profile = await profileRepository.GetByUserId(portalUser.PortalUserID);

                if (profile!=null)
                {
                    var model = await ProcessProfile(profile, portalUser, state.CurrentUserName);

                    state.Response.Profiles.Add(model);
                }
            }

            return Success();
        }
    }
}
