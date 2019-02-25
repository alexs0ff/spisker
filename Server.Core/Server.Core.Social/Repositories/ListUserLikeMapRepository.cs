using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common;
using Server.Core.Common.Contexts;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Social;
using Server.Core.Common.Repositories;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Repositories.Users;

namespace Server.Core.Social.Repositories
{
    /// <summary>
    /// Реализация репозитория с лайками пользователя.
    /// </summary>
    public class ListUserLikeMapRepository: CrudRepositoryBase<ListUserLikeMap, Guid, StartenumEntities,StartenumEntitiesFactory>, IListUserLikeMapRepository
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public ListUserLikeMapRepository(StartenumEntitiesFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Признак что список имеет лайк.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Признак наличия.</returns>
        public Task<bool> UserListHasLike(Guid listId, Guid userId)
        {
            return Entities.AnyAsync(l => l.PortalUserID == userId && l.ListID == listId);
        }

        /// <summary>
        /// Заполняет списки лайками пользователя.
        /// </summary>
        /// <param name="lists">Списко.</param>
        /// <param name="userId">Пользователь на проверку наличия лайков.</param>
        public async Task FillUserLikes(List<OwnedList> lists, Guid userId)
        {
            foreach (var ownedList in lists)
            {
                ownedList.CurrentUserHasLike = await UserListHasLike(ownedList.ListID, userId);
            }
        }

        /// <summary>
        /// Заполняет списки лайками пользователя.
        /// </summary>
        /// <param name="lists">Списко.</param>
        /// <param name="userName">Пользователь на проверку наличия лайков.</param>
        public async Task FillUserLikes(List<OwnedList> lists, string userName)
        {
            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();
            var user = await userRepository.GetByName(userName);

            if (user==null)
            {
                return;
            }

            await FillUserLikes(lists,user.PortalUserID);
        }

        /// <summary>
        /// Получает объект лайка по коду.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Объект.</returns>
        public Task<ListUserLikeMap> GetLike(Guid listId, Guid userId)
        {
            return Entities.FirstOrDefaultAsync(l => l.PortalUserID == userId && l.ListID == listId);
        }

        /// <summary>
        /// Установка лайка списку.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Код установленного лайка.</returns>
        public async Task<Guid> SetLike(Guid listId, Guid userId)
        {
            var existingLike = await GetLike(listId, userId);

            if (existingLike!=null)
            {
                return existingLike.ListUserLikeMapID;
            }

            var like = new ListUserLikeMap();
            like.CreateEventTimeUTC = DateTime.UtcNow;
            like.PortalUserID = userId;
            like.ListID = listId;
            like.ListUserLikeMapID = Guid.NewGuid();

            return await SaveNew(like);
        }

        /// <summary>
        /// Удаление лайка из списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код пользователя.</param>
        public async Task UnsetLike(Guid listId, Guid userId)
        {
            var existingLike = await GetLike(listId, userId);

            if (existingLike != null)
            {
                await Delete(existingLike.ListUserLikeMapID);
            }
        }

        /// <summary>
        /// Получает количество лайков у списка и 
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Количество лайков.</returns>
        public Task<long> GetLikeCount(Guid listId)
        {
            return Entities.LongCountAsync(i => i.ListID == listId);
        }
    }
}
