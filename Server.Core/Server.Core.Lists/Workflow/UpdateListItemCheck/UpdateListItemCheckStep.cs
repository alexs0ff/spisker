using System;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Workflow;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.UpdateListItemCheck
{
    /// <summary>
    /// Шаг обновления состояния пункта списка.
    /// </summary>
    public class UpdateListItemCheckStep:StepBase<UpdateListItemCheckParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(UpdateListItemCheckParams state)
        {
            var rep = StartEnumServer.Instance.GetRepository<IListItemRepository>();

            var listItem = await rep.GetById(state.ListItemId);
            listItem.EditEventTimeUTC = DateTime.UtcNow;
            listItem.IsChecked = state.IsChecked;

            await rep.Update(listItem);

            var model = StartEnumServer.Instance.GetMapper().Map<ListItemModel>(listItem);

            state.Response = new UpdateListItemCheckResponse
            {
                ListItem = model
            };
            return Success();
        }
    }
}
