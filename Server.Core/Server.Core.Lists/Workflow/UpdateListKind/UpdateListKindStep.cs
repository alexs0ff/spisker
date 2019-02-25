using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Sets;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.UpdateListKind
{
    /// <summary>
    /// Шаг обновления типа списка.
    /// </summary>
    public class UpdateListKindStep:StepBase<UpdateListKindParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(UpdateListKindParams state)
        {
            if (!ListKindSet.Exists(state.ListKindId))
            {
                state.Response = new ListKindIdIsNotExistsResponse(state.ListKindId);
                return Success();
            }

            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();
            await listRepository.UpdateListKindId(state.ListId, state.ListKindId);

            state.Response = new UpdateListKindResponse
            {
                ListId = state.ListId,
                ListKind = state.ListKindId
            };

            return Success();
        }
    }
}
