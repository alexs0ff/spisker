using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.RemoveListItem
{
    /// <summary>
    /// Шаг удаления пункта списка.
    /// </summary>
    public class RemoveListItemStep: StepBase<RemoveListItemParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(RemoveListItemParams state)
        {
            var repository = StartEnumServer.Instance.GetRepository<IListItemRepository>();
            var listId = await repository.GetListId(state.ListItemId);
            await repository.Delete(state.ListItemId);
            state.Response = new RemoveListItemResponse
            {
                ListItemId = state.ListItemId
            };

            if (listId!=null)
            {
                await repository.ReorderItems(listId.Value);
            }
            return Success();
        }
    }
}
