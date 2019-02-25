using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.UpdateList
{
    /// <summary>
    /// Шаг обновления списка.
    /// </summary>
    public class UpdateListStep : StepBase<UpdateListParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Состояние перехода. если состояние не меняется, тогда исполнение прекращается.</returns>
        public override async Task<StepResult> Execute(UpdateListParams state)
        {
            var profileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();
            var repository = StartEnumServer.Instance.GetRepository<IListRepository>();
            
            var list = await repository.GetById(state.ListId);

            list.Name = state.Name;

            await repository.Update(list);

            var savedList = await repository.GetOwnedList(state.User.PortalUserID, list.ListID);

            await profileRepository.FillAvatar(savedList);

            var mapped = StartEnumServer.Instance.GetMapper().Map<ListModel>(savedList);

            state.Response = new UpdateListResponse
            {
                List = mapped
            };

            return Success();
        }
    }
}
