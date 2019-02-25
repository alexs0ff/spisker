using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.GetUserFeed
{
    /// <summary>
    /// Шаг получения ленты для определенного пользователя.
    /// </summary>
    public class GetUserFeedStep:StepBase<GetUserFeedParams>
    {
        /// <summary>
        /// КОличество списков которое можно получить за один раз.
        /// </summary>
        private const int PageCount = 5;

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(GetUserFeedParams state)
        {
            Guid id;

            Guid? lastListId = null;

            if (Guid.TryParse(state.LastListId, out id))
            {
                lastListId = id;
            }

            var followingRepository = StartEnumServer.Instance.GetRepository<IUserFollowingMapRepository>();
            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();

            var socialRepository = StartEnumServer.Instance.GetRepository<IListUserLikeMapRepository>();

            followingRepository.ShareContext(listRepository);

            var users = followingRepository
                .GetUserFollowings(state.User.PortalUserID);

            var result = await listRepository.GetPagedOwnedLists(users, lastListId, PageCount, true);

            var lists = await result.OwnedLists.ToListAsync();

            await socialRepository.FillUserLikes(lists, state.User.PortalUserID);

            var userProfileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();

            await userProfileRepository.FillAvatars(lists);

            var models = StartEnumServer.Instance.GetMapper().Map<List<ListModel>>(lists);

            state.ListsResponse = new GetUserFeedResponse
            {
                Lists = models,
                HasMore = result.HasMore,
                LastListId = result.LastListId,
                UserName = state.User.UserName
            };

            return Success();
        }
    }
}
