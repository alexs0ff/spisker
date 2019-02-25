using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.UnsetLikeList
{
    /// <summary>
    /// Шаг отзыва лайка.
    /// </summary>
    public class UnsetLikeListStep:StepBase<UnsetLikeListParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(UnsetLikeListParams state)
        {
            var listLikeRepository = StartEnumServer.Instance.GetRepository<IListUserLikeMapRepository>();
            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();

            listRepository.ShareContext(listLikeRepository);

            using (var transaction = listLikeRepository.GetTransaction())
            {
                transaction.Begin();

                await listLikeRepository.UnsetLike(state.ListId, state.User.PortalUserID);

                await listRepository.DecrementLikeCount(state.ListId);

                transaction.Commit();
            }

            var count = await listLikeRepository.GetLikeCount(state.ListId);
            state.Response = new UnsetLikeListResponse
            {
                ListId = state.ListId,
                LikeCount = count
            };

            return Success();
        }
    }
}
