using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.RemoveList
{
    /// <summary>
    /// Шаг удаления списка.
    /// </summary>
    public class RemoveListStep:StepBase<RemoveListParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(RemoveListParams state)
        {
            var listRepository =  StartEnumServer.Instance.GetRepository<IListRepository>();

            var profileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();

            listRepository.ShareContext(profileRepository);

            using (var trans = listRepository.GetTransaction())
            {
                trans.Begin();

                await listRepository.MarkAsInactive(state.ListId);

                await profileRepository.DecrementListCount(state.User.PortalUserID);

                var listId = await listRepository.GetOriginalListId(state.ListId);
                if (listId!=null)
                {
                    await listRepository.DecrementRepostCount(listId.Value);
                }


                trans.Commit();
            }

            state.Response = new RemoveListResponse
            {
                ListId = state.ListId
            };
            return Success();
        }
    }
}
