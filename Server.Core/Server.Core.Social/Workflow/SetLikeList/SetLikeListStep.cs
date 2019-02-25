using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.SetLikeList
{
    /// <summary>
    /// Шаг установки лайка списка.
    /// </summary>
    public class SetLikeListStep: StepBase<SetLikeListParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(SetLikeListParams state)
        {
            var listLikeRepository = StartEnumServer.Instance.GetRepository<IListUserLikeMapRepository>();
            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();

            listRepository.ShareContext(listLikeRepository);

            using (var transaction = listLikeRepository.GetTransaction())
            {
                transaction.Begin();

                await listLikeRepository.SetLike(state.ListId, state.User.PortalUserID);

                await listRepository.IncrementLikeCount(state.ListId);

                transaction.Commit();
            }

            var count = await listLikeRepository.GetLikeCount(state.ListId);
            state.Response = new SetLikeListResponse
            {
                ListId = state.ListId,
                LikeCount = count
            };

            return Success();
        }
    }
}
