using System;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Sets;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.AddNewList
{
    /// <summary>
    /// Шаг добавления нового списка.
    /// </summary>
    public class AddNewListStep: StepBase<AddNewListParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Состояние перехода. если состояние не меняется, тогда исполнение прекращается.</returns>
        public override async Task<StepResult> Execute(AddNewListParams state)
        {
            var profileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();
            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();
            
            listRepository.ShareContext(profileRepository);

            using (var trans = listRepository.GetTransaction())
            {
                trans.Begin();

                var list = new List();
                list.PortalUserID = state.User.PortalUserID;
                list.CreateEventTimeUTC = DateTime.UtcNow;
                list.ListID = Guid.NewGuid();
                list.Name = state.Name;
                list.ListKindID = ListKindSet.Simple.ListKindID;
                list.ListCheckItemKindID = ListCheckItemKindSet.None.ListCheckItemKindID;
                await listRepository.SaveNew(list);

                var savedList = await listRepository.GetOwnedList(state.User.PortalUserID, list.ListID);

                await profileRepository.FillAvatar(savedList);

                var mapped = StartEnumServer.Instance.GetMapper().Map<ListModel>(savedList);

                state.Response = new AddNewListResponse
                {
                    List = mapped
                };

                await profileRepository.IncrementListCount(state.User.PortalUserID);

                trans.Commit();
            }

            return Success();
        }
    }
}
