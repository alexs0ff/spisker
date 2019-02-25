using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Sets;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.UpdateListCheckItemKind
{
    /// <summary>
    /// Шаг изменения типа выделения пунктов списка.
    /// </summary>
    public class UpdateListCheckItemKindStep:StepBase<UpdateListCheckItemKindParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(UpdateListCheckItemKindParams state)
        {
            if (!ListCheckItemKindSet.Exists(state.ListCheckItemKindId))
            {
                state.Response = new ListCheckItemKindIsIsNotExistsResponse(state.ListCheckItemKindId);
                return Success();
            }

            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();
            await listRepository.UpdateListCheckItemKindId(state.ListId, state.ListCheckItemKindId);

            state.Response = new UpdateListCheckItemKindResponse
            {
                ListId = state.ListId,
                ListCheckItemKind = state.ListCheckItemKindId
            };

            return Success();
        }
    }
}
