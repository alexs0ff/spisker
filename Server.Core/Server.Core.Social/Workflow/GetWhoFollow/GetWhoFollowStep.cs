using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;
using Server.Core.Social.Models;
using Server.Core.Social.Workflow.Common;

namespace Server.Core.Social.Workflow.GetWhoFollow
{
    /// <summary>
    /// Шаг получения следователей.
    /// </summary>
    public class GetWhoFollowStep: ProfileStepBase<GetWhoFollowParams>
    {
        private const int MaxCount = 10;

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(GetWhoFollowParams state)
        {
            state.Response = new GetWhoFollowResponse();
            state.Response.Profiles = new List<PortalUserProfileModel>();

            state.Response.ForUser = state.CurrentUserName;

            var followingRepository = StartEnumServer.Instance.GetRepository<IUserFollowingMapRepository>();

            var users = await followingRepository.GetFollowers(state.User.PortalUserID, state.LastFollowerId, state.Search,
                MaxCount);

            var profileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();

            foreach (var portalUser in users)
            {
                var profile = await profileRepository.GetByUserId(portalUser.PortalUserID);

                if (profile != null)
                {
                    var model = await ProcessProfile(profile, portalUser, state.CurrentUserName);

                    state.Response.Profiles.Add(model);
                    state.Response.LastFollowerId = portalUser.PortalUserID;
                }
            }

            return Success();
        }
    }
}
