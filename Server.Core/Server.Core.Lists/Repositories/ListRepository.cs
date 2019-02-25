using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Contexts;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Results;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Repositories;
using Server.Core.Common.Repositories.Filters;
using Server.Core.Common.Repositories.Lists;

namespace Server.Core.Lists.Repositories
{
    /// <summary>
    /// Реализация репозитрия для списков.
    /// </summary>
    public class ListRepository:CrudRepositoryBase<List,Guid, StartenumEntities,StartenumEntitiesFactory>,IListRepository
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public ListRepository(StartenumEntitiesFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Получает списки с пунктами.
        /// </summary>
        public IQueryable<List> GetListWithItems(DbSet<List> entities) {  return entities.Include(i=>i.ListItem); }

        /// <summary>
        /// Получает списки с пунктами.
        /// </summary>
        public IQueryable<List> ListWithItems
        {
            get { return GetListWithItems(Entities); }
        }

        /// <summary>
        /// Активные на данный момент списки.
        /// </summary>
        public IQueryable<List> ActiveLists(IQueryable<List> lists)
        {
            return lists.Where(l => !l.IsDeleted);
        }

        

        /// <summary>
        /// Получает активные списки с пунктами.
        /// </summary>
        /// <param name="entities">Списки.</param>
        /// <returns>Результат.</returns>
        public IQueryable<List> ActiveListsWithItems(DbSet<List> entities)
        {
            return GetListWithItems(entities).Filter(ActiveLists);
        }

        /// <summary>
        /// Списки с информацией о пользователе.
        /// </summary>
        public IQueryable<OwnedList> OwnedLists(IQueryable<List> lists, IQueryable<PortalUser> users)
        {
            return lists.Join(users,list => list.PortalUserID,user => user.PortalUserID, (list, user) =>
            new OwnedList
            {
                PortalUserID = user.PortalUserID,
                ListID = list.ListID,
                UserName = user.UserName,
                MiddleName = user.MiddleName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreateEventTimeUTC = list.CreateEventTimeUTC,
                IsDeleted = list.IsDeleted,
                LikeCount = list.LikeCount,
                Name = list.Name,
                PublicID = list.PublicID,
                RepostCount = list.RepostCount,
                ListItem = list.ListItem,
                OriginalListID = list.OriginalListID,
                OriginalPortalUserID = list.OriginalPortalUserID,
                OriginFirstName = list.OriginFirstName,
                OriginLastName = list.OriginLastName,
                OriginMiddleName = list.OriginMiddleName,
                OriginUserName = list.OriginUserName,
                ListCheckItemKindID = list.ListCheckItemKindID,
                ListKindID = list.ListKindID,
                IsPublished = list.IsPublished
            });
        }

        /// <summary>
        /// Получение активных записей для определенного пользователя
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Записи.</returns>
        public IQueryable<OwnedList> GetOwnedLists(Guid userId)
        {
            return OwnedLists(ActiveListsWithItems(Entities),
                GetContext().PortalUser.Where(u => u.PortalUserID == userId));
        }

        /// <summary>
        /// Получение активных записей для определенного пользователя разбитого по страницам.
        /// </summary>
        /// <param name="users">Пользователи для которых собираются списки.</param>
        /// <param name="afterListId">Код списка после которого ведется выборка.</param>
        /// <param name="listsCount">Количиство списков в выборке.</param>
        /// <param name="onlyPublished">Признак того, что нужно выводить только опубликованные списки.</param>
        /// <returns>Записи.</returns>
        public async Task<PagedOwnedListResult> GetPagedOwnedLists(IQueryable<PortalUser> users, Guid? afterListId, int listsCount, bool onlyPublished)
        {
            PagedOwnedListResult result = new PagedOwnedListResult();

            long? publicId = null;
            if (afterListId != null)
            {
                publicId = await Entities.Join(users, list => list.PortalUserID,guid => guid.PortalUserID,(list, guid) => list).Where(l=>l.ListID== afterListId.Value).Select(i=>(long?)i.PublicID).FirstOrDefaultAsync();
            }

            var items = ActiveLists(Entities).PublishedFilter(onlyPublished)
                .Join(users, list => list.PortalUserID,userId => userId.PortalUserID,(list, guid) => list);

            if (publicId!=null)
            {
                items = items.Where(l=>l.PublicID < publicId.Value);
            }

            var takeItems = await items
                .OrderByDescending(l => l.PublicID)
                .Take(listsCount)
                .Select(i => new {i.ListID, i.PublicID}).ToListAsync();

            var item = takeItems.LastOrDefault();

            long? lastPublic = null;
            if (item != null)
            {
                result.LastListId = item.ListID;
                lastPublic = item.PublicID;

                result.HasMore = await ActiveLists(Entities).PublishedFilter(onlyPublished)
                    .Join(users, list => list.PortalUserID, userId => userId.PortalUserID, (list, guid) => list)
                    .AnyAsync(l => l.PublicID < lastPublic);
            }
            else
            {
                result.HasMore = false;
            }

            var ownedLists = OwnedLists(ActiveListsWithItems(Entities).PublishedFilter(onlyPublished), users);

            if (publicId!=null)
            {
                ownedLists = ownedLists.Where(l => l.PublicID < publicId.Value);
            }

            if (lastPublic!=null)
            {
                ownedLists = ownedLists.Where(l => l.PublicID >= lastPublic.Value);
            }
            result.OwnedLists = ownedLists.OrderByDescending(l=>l.PublicID).Take(listsCount);

            return result;
        }

        /// <summary>
        /// Получение активной записи для определенного пользователя с определенным кодом.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="listId">Код списка.</param>
        /// <returns>Запись.</returns>
        public Task<OwnedList> GetOwnedList(Guid userId,Guid listId)
        {
            return GetOwnedLists(userId).Where(l => l.ListID == listId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получение активной записи с определенным кодом.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Запись.</returns>
        public Task<OwnedList> GetOwnedList(Guid listId)
        {
            return OwnedLists(ActiveListsWithItems(Entities),
                GetContext().PortalUser).FirstOrDefaultAsync(i=>i.ListID==listId);
        }

        /// <summary>
        /// Определяет существование активного списка по коду.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Признак существования.</returns>
        public Task<bool> ActiveListExists(Guid listId)
        {
            return ActiveLists(Entities).AnyAsync(l => l.ListID == listId);
        }

        /// <summary>
        /// Определяет существование активного списка пользователя по коду.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код Пользователя.</param>
        /// <returns>Признак существования.</returns>
        public Task<bool> ActiveListExists(Guid listId, Guid userId)
        {
            return ActiveLists(Entities).AnyAsync(l => l.ListID == listId && l.PortalUserID == userId);
        }

        /// <summary>
        /// Помечает список как неактивный.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Результат.</returns>
        public Task<int> MarkAsInactive(Guid listId)
        {
            const string updateIsDeletedSql = @"UPDATE [dbo].[List]
               SET 
                  [IsDeleted] = 1
            WHERE 
            ListID = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(updateIsDeletedSql, listId);
        }

        #region Like count 

        private Task AddLikeCount(Guid userId, int number)
        {
            const string sql = @"
            UPDATE [dbo].[List]
            SET [LikeCount] = LikeCount + {0}    
            WHERE ListID = {1}";
            return GetContext().Database.ExecuteSqlCommandAsync(sql, number, userId);
        }

        /// <summary>
        /// Повышает на единицу количество лайков для списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        public Task IncrementLikeCount(Guid listId)
        {
            return AddLikeCount(listId, 1);
        }

        /// <summary>
        /// Уменьшает на единицу количество лайков в списке.
        /// </summary>
        /// <param name="listId">Код пользователя.</param>
        public Task DecrementLikeCount(Guid listId)
        {
            return AddLikeCount(listId, -1);
        }

        #endregion count

        #region Repost count 

        private Task AddRepostCount(Guid userId, int number)
        {
            const string sql = @"
            UPDATE [dbo].[List]
            SET [RepostCount] = RepostCount + {0}    
            WHERE ListID = {1}";
            return GetContext().Database.ExecuteSqlCommandAsync(sql, number, userId);
        }

        /// <summary>
        /// Повышает на единицу количество репостов для списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        public Task IncrementRepostCount(Guid listId)
        {
            return AddRepostCount(listId, 1);
        }

        /// <summary>
        /// Уменьшает на единицу количество репостов в списке.
        /// </summary>
        /// <param name="listId">Код пользователя.</param>
        public Task DecrementRepostCount(Guid listId)
        {
            return AddRepostCount(listId, -1);
        }


        /// <summary>
        /// Получает количество репостов у списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Количество репостов.</returns>
        public Task<long> GetRepostCount(Guid listId)
        {
            return Entities.Where(l=>l.ListID==listId).Select(i=>i.RepostCount).FirstOrDefaultAsync();
        }

        #endregion count
        
        /// <summary>
        /// Получает код репостнутого списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Код репостнутого списка.</returns>
        public Task<Guid?> GetOriginalListId(Guid listId)
        {
            return Entities.Where(l => l.ListID == listId).Select(l => l.OriginalListID).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Обновляет тип списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="listKindId">Новый код типа списка.</param>
        /// <returns>Результат.</returns>
        public Task<int> UpdateListKindId(Guid listId, int listKindId)
        {
            const string updateSql = @"UPDATE [dbo].[List]
               SET 
                  [ListKindID] = {1}
            WHERE 
            ListID = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(updateSql, listId, listKindId);
        }

        /// <summary>
        /// Обновляет тип выделения пунктов списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="listCheckItemKindId">Новый код выделения пунктов списка.</param>
        /// <returns>Результат.</returns>
        public Task<int> UpdateListCheckItemKindId(Guid listId, int listCheckItemKindId)
        {
            const string updateSql = @"UPDATE [dbo].[List]
               SET 
                  [ListCheckItemKindID] = {1}
            WHERE 
            ListID = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(updateSql, listId, listCheckItemKindId);
        }

        /// <summary>
        /// Получает код списка определенного пользователя по его публичному коду.
        /// </summary>
        /// <param name="ownerId">Код владельца.</param>
        /// <param name="publicId">Код списка.</param>
        /// <returns>Код списка.</returns>
        public async Task<Guid?> GetListId(Guid ownerId,long publicId)
        {
            Guid? guid= await Entities.Where(i => i.PortalUserID == ownerId && i.PublicID == publicId).Select(i=>i.ListID).FirstOrDefaultAsync();

            return guid;
        }

        /// <summary>
        /// Обновляет тип публикации списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="isPublished">Новый признак публикации списка.</param>
        /// <returns>Результат.</returns>
        public Task<int> UpdateListPublished(Guid listId, bool isPublished)
        {
            const string updateSql = @"UPDATE [dbo].[List]
               SET 
                  [IsPublished] = {1}
            WHERE 
            ListID = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(updateSql, listId, isPublished);
        }


    }
}
