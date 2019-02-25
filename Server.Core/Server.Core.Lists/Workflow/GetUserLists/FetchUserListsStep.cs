using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.GetUserLists
{
    /// <summary>
    /// Шаг получения ответа по выборки пользователей.
    /// </summary>
    public class FetchUserListsStep: StepBase<FetchUserListsParams>
    {
        /// <summary>
        /// КОличество списков которое можно получить за один раз.
        /// </summary>
        private const int PageCount = 5;

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Состояние перехода. если состояние не меняется, тогда исполнение прекращается.</returns>
        public override async Task<StepResult> Execute(FetchUserListsParams state)
        {
            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();
            Guid id;

            Guid? lastListId = null;

            Guid? selectedListId = null;

            if (Guid.TryParse(state.LastListId, out id))
            {
                lastListId = id;
            }

            if (lastListId == null)
            {
                if (state.SelectedListNumber!=default(long))
                {
                    lastListId = await listRepository.GetListId(state.User.PortalUserID, state.SelectedListNumber);
                    selectedListId = lastListId;
                }
            }

            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();

            var userProfileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();

            var socialRepository = StartEnumServer.Instance.GetRepository<IListUserLikeMapRepository>();

            listRepository.ShareContext(userRepository);

            var user =
                userRepository.GetPortalUsers(state.User.PortalUserID);

            
            var result = await listRepository.GetPagedOwnedLists(user, lastListId, PageCount, state.ForUserId != state.User.PortalUserID);

            var lists = await result.OwnedLists.ToListAsync();

            if (selectedListId!=null)
            {
                var list = await listRepository.GetOwnedList(selectedListId.Value);
                if (list!=null)
                {
                    lists.Insert(0, list);
                }
            }

            if (!string.IsNullOrWhiteSpace(state.ForUserName))
            {
                await socialRepository.FillUserLikes(lists, state.ForUserName);
            }

            await userProfileRepository.FillAvatars(lists);

            var models = StartEnumServer.Instance.GetMapper().Map<List<ListModel>>(lists);

            state.ListsResponse = new GetUserListsResponse
            {
                Lists = models,
                HasMore = result.HasMore,
                LastListId = result.LastListId,
                SelectedListId = selectedListId
            };

            return Success();
        }
    }
}
