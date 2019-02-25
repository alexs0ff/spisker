using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckUserExists;

namespace Server.Core.Social.Workflow.StopFollowing
{
    /// <summary>
    /// Шаг отписки одного пользователя от другого.
    /// </summary>
    public class StopFollowingStep : StepBase<StopFollowingParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(StopFollowingParams state)
        {
            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();
            var toUser = await userRepository.GetByName(state.ToUserName);

            if (toUser == null)
            {
                return ToFinish<UserNotFoundStep>();
            }

            var followRepository = StartEnumServer.Instance.GetRepository<IUserFollowingMapRepository>();
            var profileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();
            profileRepository.ShareContext(followRepository);

            using (var trans = profileRepository.GetTransaction())
            {
                trans.Begin();

                await followRepository.Delete(state.User.PortalUserID, toUser.PortalUserID);

                await profileRepository.DecrementFollowerCount(toUser.PortalUserID);
                await profileRepository.DecrementFollowingCount(state.User.PortalUserID);

                trans.Commit();
            }

            state.Response = new StopFollowingResponse
            {
                ToUserId = toUser.PortalUserID,
                SubscriberUserId = state.User.PortalUserID
            };

            return Success();
        }
    }
}
