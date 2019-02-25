using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Logger;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckListExists;
using Server.Core.Lists.Models;
using Server.Core.Lists.Workflow.CheckListItemExists;

namespace Server.Core.Lists.Workflow.MoveToListItem
{
    /// <summary>
    /// Шаг по перемещению пункта списка.
    /// </summary>
    public class MoveToListItemStep:StepBase<MoveToListItemParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(MoveToListItemParams state)
        {
            if (string.Equals(state.AfterListItemId,state.ListItemId,StringComparison.OrdinalIgnoreCase))
            {
                state.IdNotFound = state.ListItemId;
                return ToFinish<ListItemNotFoundStep>();
            }

            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();
            var listItemRepository = StartEnumServer.Instance.GetRepository<IListItemRepository>();

            listRepository.ShareContext(listItemRepository);
            using (var transaction = listRepository.GetTransaction())
            {
                transaction.Begin();

                ListItem listItem = await GetListItem(state.ListItemId, state.User, listItemRepository);

                if (listItem == null)
                {
                    state.IdNotFound = state.ListItemId;
                    return ToFinish<ListItemNotFoundStep>();
                }

                var afterListItem = await GetListItem(state.AfterListItemId, state.User, listItemRepository);

                var targetList = await GetList(state.TargetListId, state.User, listRepository);

                if (afterListItem == null && targetList == null)
                {
                    state.IdNotFound = state.AfterListItemId;
                    return ToFinish<ListItemNotFoundStep>();
                }

                if (afterListItem != null && targetList != null && afterListItem.ListID != targetList.ListID)
                {
                    state.IdNotFound = state.TargetListId;
                    return ToFinish<ListNotFoundStep>();
                }

                var savedListItem = await ProcessMove(state, listItem, afterListItem, targetList, listItemRepository, listRepository);

                state.Response = new MoveToListItemResponse
                {
                    ListItemId = listItem.ListItemID
                };

                if (savedListItem!=null)
                {
                    state.Response.ListItem = StartEnumServer.Instance.GetMapper().Map<ListItemModel>(savedListItem);
                }

                transaction.Commit();
            }

            return Success();
        }

        private async Task<ListItem> ProcessMove(MoveToListItemParams state,ListItem listItem, ListItem afterListItem, OwnedList targetList, IListItemRepository listItemRepository, IListRepository listRepository)
        {
            ListItem result = null;

            ListItem processedListItem;
            var positions = new List<Guid>();
            var processedList = targetList;

            Guid listId;

            if (state.Copy)
            {
                processedListItem = listItemRepository.CreateNew(listItem.Content, state.User.PortalUserID);

                if (targetList != null)
                {
                    processedListItem.ListID = targetList.ListID;
                }
                else
                {
                    processedListItem.ListID = afterListItem.ListItemID;
                    processedList = await listRepository.GetOwnedList(processedListItem.ListID);
                }

                await listItemRepository.SaveNew(processedListItem);

                result = processedListItem;

                foreach (var item in processedList.ListItem.OrderBy(i => i.OrderPosition))
                {
                    positions.Add(item.ListItemID);
                }

                listId = processedList.ListID;

                ReorderPositions(afterListItem, positions, processedListItem.ListItemID);
            }
            else
            {
                processedList = targetList;
                if (targetList!=null && targetList.ListID!=listItem.ListID)
                {
                    await listItemRepository.Delete(listItem.ListItemID);
                    await listItemRepository.ReorderItems(listItem.ListID);
                }

                if (processedList == null)
                {
                    processedList = await listRepository.GetOwnedList(afterListItem.ListID);
                }

                listId = processedList.ListID;

                if (processedList.ListID!=listItem.ListID)
                {
                    listItem.ListID = processedList.ListID;
                    result = listItem;
                    await listItemRepository.SaveNew(listItem);
                }

                foreach (var item in processedList.ListItem.OrderBy(i => i.OrderPosition))
                {
                    positions.Add(item.ListItemID);
                }

                if (positions.Contains(listItem.ListItemID))
                {
                    positions.Remove(listItem.ListItemID);
                }

                ReorderPositions(afterListItem, positions, listItem.ListItemID);
            }

            await listItemRepository.ReorderItems(listId, positions);

            return result;
        }

        private static void ReorderPositions(ListItem afterListItem, List<Guid> positions, Guid listItemId)
        {
            if (afterListItem == null)
            {
                positions.Insert(0, listItemId);
            }
            else
            {
                var index = positions.IndexOf(afterListItem.ListItemID);

                if (index >= 0)
                {
                    positions.Insert(index + 1, listItemId);
                }
            }
        }

        private static async Task<OwnedList> GetList(string listId, PortalUser user, IListRepository listRepository)
        {
            Guid id; var logger = StartEnumServer.Instance.GetLogger();

            OwnedList targetList = null;

            if (Guid.TryParse(listId, out id))
            {
                targetList = await listRepository.GetOwnedList(id);
                if (targetList.PortalUserID != user.PortalUserID)
                {
                    logger.LogError<MoveToListItemStep>(
                        $"Список {id} не принадлежит пользователю {user.PortalUserID}",
                        new LoggerParameter("ListId", id),
                        new LoggerParameter("PortalUserID", user.PortalUserID));
                    targetList = null;
                }
            }

            return targetList;
        }

        private static async Task<ListItem> GetListItem(string listItemId,PortalUser user, IListItemRepository listItemRepository)
        {
            var logger = StartEnumServer.Instance.GetLogger();
            ListItem listItem =null;
            Guid id;

            if (Guid.TryParse(listItemId, out id))
            {
                listItem = await listItemRepository.GetById(id);
                if (listItem != null)
                {
                    if (listItem.PortalUserID != user.PortalUserID)
                    {
                        logger.LogError<MoveToListItemStep>(
                            $"Пункт списка {id} не принадлежит пользователю {user.PortalUserID}",
                            new LoggerParameter("ListItemId", id),
                            new LoggerParameter("PortalUserID", user.PortalUserID));
                        listItem = null;
                    }
                }
            }

            return listItem;
        }
    }
}
