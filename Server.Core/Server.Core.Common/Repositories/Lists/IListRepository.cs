using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Results;
using Server.Core.Common.Entities.Users;

namespace Server.Core.Common.Repositories.Lists
{
    /// <summary>
    /// Репозиторий списков.
    /// </summary>
    public interface IListRepository:ICrudRepository<List,Guid>
    {
        /// <summary>
        /// Активные на данный момент списки.
        /// </summary>
        IQueryable<List> ActiveLists(IQueryable<List> lists);

        /// <summary>
        /// Получает активные списки с пунктами.
        /// </summary>
        /// <param name="entities">Списки.</param>
        /// <returns>Результат.</returns>
        IQueryable<List> ActiveListsWithItems(DbSet<List> entities);

        /// <summary>
        /// Получает списки с пунктами.
        /// </summary>
        IQueryable<List> GetListWithItems(DbSet<List> entities);

        /// <summary>
        /// Получение активной записи для определенного пользователя с определенным кодом.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="listId">Код списка.</param>
        /// <returns>Запись.</returns>
        Task<OwnedList> GetOwnedList(Guid userId,Guid listId);

        /// <summary>
        /// Определяет существование активного списка по коду.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Признак существования.</returns>
        Task<bool> ActiveListExists(Guid listId);

        /// <summary>
        /// Помечает список как неактивный.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Результат.</returns>
        Task<int> MarkAsInactive(Guid listId);

        /// <summary>
        /// Получает списки с пунктами.
        /// </summary>
        IQueryable<List> ListWithItems { get; }

        /// <summary>
        /// Получение активной записи с определенным кодом.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Запись.</returns>
        Task<OwnedList> GetOwnedList(Guid listId);

        /// <summary>
        /// Получение активных записей для определенного пользователя разбитого по страницам.
        /// </summary>
        /// <param name="users">Пользователи для которых собираются списки.</param>
        /// <param name="afterListId">Код списка после которого ведется выборка.</param>
        /// <param name="listsCount">Количиство списков в выборке.</param>
        /// <param name="onlyPublished">Признак того, что нужно выводить только опубликованные списки.</param>
        /// <returns>Записи.</returns>
        Task<PagedOwnedListResult> GetPagedOwnedLists(IQueryable<PortalUser> users, Guid? afterListId,int listsCount, bool onlyPublished);

        /// <summary>
        /// Повышает на единицу количество лайков для списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        Task IncrementLikeCount(Guid listId);

        /// <summary>
        /// Уменьшает на единицу количество лайков в списке.
        /// </summary>
        /// <param name="listId">Код пользователя.</param>
        Task DecrementLikeCount(Guid listId);

        /// <summary>
        /// Повышает на единицу количество репостов для списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        Task IncrementRepostCount(Guid listId);

        /// <summary>
        /// Уменьшает на единицу количество репостов в списке.
        /// </summary>
        /// <param name="listId">Код пользователя.</param>
        Task DecrementRepostCount(Guid listId);

        /// <summary>
        /// Получает количество репостов у списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Количество репостов.</returns>
        Task<long> GetRepostCount(Guid listId);

        /// <summary>
        /// Получает код репостнутого списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Код репостнутого списка.</returns>
        Task<Guid?> GetOriginalListId(Guid listId);

        /// <summary>
        /// Обновляет тип списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="listKindId">Новый код типа списка.</param>
        /// <returns>Результат.</returns>
        Task<int> UpdateListKindId(Guid listId, int listKindId);

        /// <summary>
        /// Определяет существование активного списка пользователя по коду.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код Пользователя.</param>
        /// <returns>Признак существования.</returns>
        Task<bool> ActiveListExists(Guid listId, Guid userId);

        /// <summary>
        /// Обновляет тип выделения пунктов списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="listCheckItemKindId">Новый код выделения пунктов списка.</param>
        /// <returns>Результат.</returns>
        Task<int> UpdateListCheckItemKindId(Guid listId, int listCheckItemKindId);

        /// <summary>
        /// Получает код списка определенного пользователя по его публичному коду.
        /// </summary>
        /// <param name="ownerId">Код владельца.</param>
        /// <param name="publicId">Код списка.</param>
        /// <returns>Код списка.</returns>
        Task<Guid?> GetListId(Guid ownerId,long publicId);

        /// <summary>
        /// Обновляет тип публикации списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="isPublished">Новый признак публикации списка.</param>
        /// <returns>Результат.</returns>
        Task<int> UpdateListPublished(Guid listId, bool isPublished);
    }
}
