using System;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.CheckListItemExists
{
    /// <summary>
    /// Шаг проверки существования пункта списка.
    /// </summary>
    public class CheckListItemExistsStep : StepBase<CheckListItemExistsParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(CheckListItemExistsParams state)
        {
            Guid listItemId;

            if (!Guid.TryParse(state.ListItemId, out listItemId))
            {
                return ToFinish<ListItemNotFoundStep>();
            }

            var exists = await StartEnumServer.Instance.GetRepository<IListItemRepository>()
                .CheckExists(state.ListId, listItemId);

            if (!exists)
            {
                return ToFinish<ListItemNotFoundStep>();
            }

            return Success();
        }
    }
}
