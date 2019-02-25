using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Contexts;
using Server.Core.Common.Entities.Social;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Repositories;
using Server.Core.Common.Repositories.Social;

namespace Server.Core.Social.Repositories
{
    /// <summary>
    /// Реализация репозитория по сопоставлению пользователей.
    /// </summary>
    public class UserFollowingMapRepository : CrudRepositoryBase<UserFollowingMap, Guid, StartenumEntities,StartenumEntitiesFactory>, IUserFollowingMapRepository
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public UserFollowingMapRepository(StartenumEntitiesFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Признак тогда что пользователь подписан на другого пользователя.
        /// </summary>
        /// <param name="userId">Пользователь который проверяется.</param>
        /// <param name="followToUserId">Пользователь за которого подписан.</param>
        /// <returns>Признак.</returns>
        public Task<bool> IsFollowing(Guid userId, Guid followToUserId)
        {
                return Entities.AnyAsync(i => i.MasterID == userId && i.ChildID == followToUserId);
        }

        /// <summary>
        /// Удаление из таблицы подписчиков признака следования одного пользователя за другим.
        /// </summary>
        /// <param name="userId">Пользователь который проверяется.</param>
        /// <param name="followToUserId">Пользователь за которого подписан.</param>
        /// <returns>Количество удаленных записей</returns>
        public async Task<long> Delete(Guid userId, Guid followToUserId)
        {
            var fol = await Entities.FirstOrDefaultAsync(i => i.MasterID == userId && i.ChildID == followToUserId);

            if (fol==null)
            {
                return 0;
            }

            var context = GetContext();

            context.Set<UserFollowingMap>().Remove(fol);
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает список пользователей за которым пользователь следит.
        /// </summary>
        /// <param name="userId">Код пользователя у которого необходимо получить всех преследуемых.</param>
        /// <returns>Список пользователей.</returns>
        public IQueryable<PortalUser> GetUserFollowings(Guid userId)
        {
            return Entities.Where(i=>i.MasterID==userId).Join(GetContext().PortalUser, map => map.ChildID, user => user.PortalUserID,
                (map, user) => user);
        }

        /// <summary>
        /// Получает список пользователей которые следят за пользователем.
        /// </summary>
        /// <param name="userId">Код пользователя у которого нужно получить последователей.</param>
        /// <param name="count">Количество.</param>
        /// <param name="lastFollowerId">Код последнего следователя (для нужд постраничной выборки).</param>
        /// <param name="search">Поиск в ФИО и имени.</param>
        /// <returns>Список пользователей.</returns>
        public async Task<IQueryable<PortalUser>> GetFollowers(Guid userId,Guid? lastFollowerId,string search, int count)
        {
            DateTime? lastFolloverDateTime = null;

            if (lastFollowerId!=null)
            {
                lastFolloverDateTime = await Entities.Where(i => i.ChildID == userId && i.MasterID == lastFollowerId)
                    .Select(i => i.CreateDate).FirstOrDefaultAsync();
            }

            var query = Entities.Where(i => i.ChildID == userId);
            if (lastFolloverDateTime!=null)
            {
                query = query.Where(i => i.CreateDate >= lastFolloverDateTime.Value && i.MasterID != lastFollowerId);
            }


            var join = query.Join(GetContext().PortalUser, map => map.MasterID, user => user.PortalUserID,
                (map, user) => new {map, user});

            if (!string.IsNullOrWhiteSpace(search))
            {
                join = join.Where(u => (u.user.LastName + u.user.FirstName + u.user.UserName).Contains(search));
            }

            return join.OrderBy(i => i.map.CreateDate).Select(i => i.user).Take(count);
        }

        /// <summary>
        /// Получает список подписок пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя у которого нужно получить подписки.</param>
        /// <param name="count">Количество.</param>
        /// <param name="lastFollowingId">Код последней подписки (для нужд постраничной выборки).</param>
        /// <param name="search">Поиск в ФИО и имени.</param>
        /// <returns>Список пользователей.</returns>
        public async Task<IQueryable<PortalUser>> GetFollowings(Guid userId, Guid? lastFollowingId, string search, int count)
        {
            DateTime? lastFolloverDateTime = null;

            if (lastFollowingId != null)
            {
                lastFolloverDateTime = await Entities.Where(i => i.MasterID == userId && i.ChildID == lastFollowingId)
                    .Select(i => i.CreateDate).FirstOrDefaultAsync();
            }

            var query = Entities.Where(i => i.MasterID == userId);
            if (lastFolloverDateTime != null)
            {
                query = query.Where(i => i.CreateDate >= lastFolloverDateTime.Value && i.ChildID != lastFollowingId);
            }


            var join = query.Join(GetContext().PortalUser, map => map.ChildID, user => user.PortalUserID,
                (map, user) => new { map, user });

            if (!string.IsNullOrWhiteSpace(search))
            {
                join = join.Where(u => (u.user.LastName + u.user.FirstName + u.user.UserName).Contains(search));
            }

            return join.OrderBy(i => i.map.CreateDate).Select(i => i.user).Take(count);
        }
    }
}
