using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Contexts;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Repositories;
using Server.Core.Common.Repositories.Lists;

namespace Server.Core.Lists.Repositories
{
    /// <summary>
    /// Реализация репозитория для пунктов списка.
    /// </summary>
    public class ListItemRepository: CrudRepositoryBase<ListItem,Guid, StartenumEntities, StartenumEntitiesFactory>, IListItemRepository
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public ListItemRepository(StartenumEntitiesFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Получает Количество пунктов у определенного списка.
        /// </summary>
        /// <param name="listId">КОд списка.</param>
        /// <returns>Количество.</returns>
        public Task<int> GetItemsCount(Guid listId)
        {
            return Entities.CountAsync(i => i.ListID == listId);
        }

        /// <summary>
        /// Проверка на существования пункта списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="listItemId">Код пункта списка.</param>
        /// <returns>Результат.</returns>
        public Task<bool> CheckExists(Guid listId,Guid listItemId)
        {
            return Entities.AnyAsync(i => i.ListID == listId && i.ListItemID == listItemId);
        }

        /// <summary>
        /// Создает новый пункт репозитория, но не сохраняет в базе.
        /// </summary>
        /// <param name="content">Контент.</param>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>СОзданный список.</returns>
        public ListItem CreateNew(string content, Guid userId)
        {
            var item = new ListItem();
            item.PortalUserID = userId;
            item.Content = content;
            item.CreateEventTimeUTC = DateTime.UtcNow;
            item.EditEventTimeUTC = DateTime.UtcNow;
            item.ListItemID = Guid.NewGuid();

            return item;
        }

        /// <summary>
        /// Получает код списка у определенного пункта списка.
        /// </summary>
        /// <param name="listItemId">Код пункта списка.</param>
        /// <returns>Код списка.</returns>
        public Task<Guid?> GetListId(Guid listItemId)
        {
            return Entities.Where(i => i.ListItemID == listItemId).Select(i => (Guid?) i.ListID).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Переорганизовывает пункты списков согласно их позиции.
        /// </summary>
        /// <param name="itemIds">Коды пунктов.</param>
        /// <param name="listId">Код списка.</param>
        /// <returns>Результат.</returns>
        public async Task ReorderItems(Guid listId,List<Guid> itemIds)
        {
            string[] arr = itemIds.Select(i => string.Concat(" SELECT '", i, "' ,", itemIds.IndexOf(i))).ToArray();
            var tempTableFill = string.Join(" UNION ALL ",arr);
            var sql = string.Format(ReorderTableExpSql, tempTableFill);

            await GetContext().Database.ExecuteSqlCommandAsync(sql, listId);
        }

        private const string ReorderTableExpSql = @"
declare @t table (id uniqueidentifier, position int)
insert into @t
{0}

UPDATE li
Set li.OrderPosition = t.position
FROM [dbo].[ListItem] li
Inner join @t t 
ON li.ListItemId  = t.id
where li.ListID = {{0}}
";

        /// <summary>
        /// Переорганизовывает пункты списков согласно их позиции.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Результат.</returns>
        public async Task ReorderItems(Guid listId)
        {
            var guids = await GetListItemsId(listId);

            await ReorderItems(listId, guids);
        }

        /// <summary>
        /// Получает список кодов пунктов списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Список кодов пунктов.</returns>
        public Task<List<Guid>> GetListItemsId(Guid listId)
        {
            return Entities.Where(i => i.ListID == listId).OrderBy(l => l.OrderPosition).Select(i => i.ListItemID).ToListAsync();
        }

        /// <summary>
        /// Получает пункты списка по коду списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Пункты списка.</returns>
        public IEnumerable<ListItem> GetItemsByListId(Guid listId)
        {
            return Entities.Where(i => i.ListID == listId);
        }
    }
}
