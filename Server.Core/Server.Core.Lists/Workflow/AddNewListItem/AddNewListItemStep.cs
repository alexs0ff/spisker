using System;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Workflow;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.AddNewListItem
{
    /// <summary>
    /// Шаг добавления пункта списка.
    /// </summary>
    public class AddNewListItemStep : StepBase<AddNewListItemParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(AddNewListItemParams state)
        {
            Guid afterListItemId;

            if (!Guid.TryParse(state.AfterListItemId,out afterListItemId))
            {
                afterListItemId = Guid.Empty;
            }
            var repository = StartEnumServer.Instance.GetRepository<IListItemRepository>();

            var item = repository.CreateNew(state.Content, state.User.PortalUserID);
            item.ListID = state.ListId;

            item.OrderPosition = 0;
            await repository.SaveNew(item);

            var ids = await repository.GetListItemsId(item.ListID);
            ids.Remove(item.ListItemID);

            if (afterListItemId!=Guid.Empty)
            {
                var index = ids.IndexOf(afterListItemId);
                if (index>=0)
                {
                    ids.Insert(index + 1, item.ListItemID);
                }
                else
                {
                    ids.Insert(0, item.ListItemID);
                }
            }
            else
            {
                ids.Insert(0, item.ListItemID);
            }

            await repository.ReorderItems(item.ListID, ids);

            repository.Detach(item);

            var savedItem = await repository.GetById(item.ListItemID);

            var mapped = StartEnumServer.Instance.GetMapper().Map<ListItemModel>(savedItem);

            state.Response = new AddNewListItemResponse
            {
                ListItem = mapped
            };

            return Success();
        }
    }
}
