using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Core.Common.Entities.Lists;

namespace Server.Core.Common.Repositories.Lists
{
    /// <summary>
    /// Интерфейс для работы с пунктами списка.
    /// </summary>
    public interface IListItemRepository : ICrudRepository<ListItem, Guid>
    {
        /// <summary>
        /// Получает Количество пунктов у определенного списка.
        /// </summary>
        /// <param name="listId">КОд списка.</param>
        /// <returns>Количество.</returns>
        Task<int> GetItemsCount(Guid listId);

        /// <summary>
        /// Проверка на существования пункта списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="listItemId">Код пункта списка.</param>
        /// <returns>Результат.</returns>
        Task<bool> CheckExists(Guid listId,Guid listItemId);

        /// <summary>
        /// Создает новый пункт репозитория, но не сохраняет в базе.
        /// </summary>
        /// <param name="content">Контент.</param>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>СОзданный список.</returns>
        ListItem CreateNew(string content, Guid userId);

        /// <summary>
        /// Переорганизовывает пункты списков согласно их позиции.
        /// </summary>
        /// <param name="itemIds">Коды пунктов.</param>
        /// <param name="listId">Код списка.</param>
        /// <returns>Результат.</returns>
        Task ReorderItems(Guid listId,List<Guid> itemIds);

        /// <summary>
        /// Переорганизовывает пункты списков согласно их позиции.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Результат.</returns>
        Task ReorderItems(Guid listId);

        /// <summary>
        /// Получает код списка у определенного пункта списка.
        /// </summary>
        /// <param name="listItemId">Код пункта списка.</param>
        /// <returns>Код списка.</returns>
        Task<Guid?> GetListId(Guid listItemId);

        /// <summary>
        /// Получает список кодов пунктов списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Список кодов пунктов.</returns>
        Task<List<Guid>> GetListItemsId(Guid listId);

        /// <summary>
        /// Получает пункты списка по коду списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Пункты списка.</returns>
        IEnumerable<ListItem> GetItemsByListId(Guid listId);
    }
}
