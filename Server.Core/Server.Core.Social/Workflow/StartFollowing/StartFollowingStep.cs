using System;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Social;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckUserExists;

namespace Server.Core.Social.Workflow.StartFollowing
{
    /// <summary>
    /// Шаг подписки на списки пользователя.
    /// </summary>
    public class StartFollowingStep : StepBase<StartFollowingParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(StartFollowingParams state)
        {
            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();
            
            var toUser = await userRepository.GetByName(state.ToUserName);

            if (toUser==null)
            {
                return ToFinish<UserNotFoundStep>();
            }

            var followRepository = StartEnumServer.Instance.GetRepository<IUserFollowingMapRepository>();
            var profileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();
            profileRepository.ShareContext(followRepository);

            using (var trans = profileRepository.GetTransaction())
            {
                trans.Begin();

                await profileRepository.IncrementFollowerCount(toUser.PortalUserID);
                await profileRepository.IncrementFollowingCount(state.User.PortalUserID);

                var following = new UserFollowingMap();

                following.MasterID = state.User.PortalUserID;
                following.ChildID = toUser.PortalUserID;
                following.CreateDate = DateTime.UtcNow;
                following.UserFollowingMapID = Guid.NewGuid();

                await followRepository.SaveNew(following);
                
                trans.Commit();
            }

            state.Response = new StartFollowingResponse
            {
                ToUserId = toUser.PortalUserID,
                SubscriberUserId = state.User.PortalUserID
            };

            return Success();
        }
    }
}
