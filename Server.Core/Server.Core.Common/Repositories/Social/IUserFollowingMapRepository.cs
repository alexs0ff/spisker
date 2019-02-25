using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Core.Common.Entities.Social;
using Server.Core.Common.Entities.Users;

namespace Server.Core.Common.Repositories.Social
{
    /// <summary>
    /// Интерфейс для сопоставлений пользователей.
    /// </summary>
    public interface IUserFollowingMapRepository : ICrudRepository<UserFollowingMap, Guid>
    {
        /// <summary>
        /// Признак тогда что пользователь подписан на другого пользователя.
        /// </summary>
        /// <param name="userId">Пользователь который проверяется.</param>
        /// <param name="followToUserId">Пользователь за которого подписан.</param>
        /// <returns>Признак.</returns>
        Task<bool> IsFollowing(Guid userId, Guid followToUserId);

        /// <summary>
        /// Удаление из таблицы подписчиков признака следования одного пользователя за другим.
        /// </summary>
        /// <param name="userId">Пользователь который проверяется.</param>
        /// <param name="followToUserId">Пользователь за которого подписан.</param>
        /// <returns>Количество удаленных записей</returns>
        Task<long> Delete(Guid userId, Guid followToUserId);

        /// <summary>
        /// Получает список пользователей за которым пользователь следит.
        /// </summary>
        /// <param name="userId">Код пользователя у которого необходимо получить всех преследуемых.</param>
        /// <returns>Список пользователей.</returns>
        IQueryable<PortalUser> GetUserFollowings(Guid userId);

        /// <summary>
        /// Получает список пользователей которые следят за пользователем.
        /// </summary>
        /// <param name="userId">Код пользователя у которого нужно получить последователей.</param>
        /// <param name="count">Количество.</param>
        /// <param name="lastFollowerId">Код последнего следователя (для нужд постраничной выборки).</param>
        /// <param name="search">Поиск в ФИО и имени.</param>
        /// <returns>Список пользователей.</returns>
        Task<IQueryable<PortalUser>> GetFollowers(Guid userId,Guid? lastFollowerId,string search, int count);

        /// <summary>
        /// Получает список подписок пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя у которого нужно получить подписки.</param>
        /// <param name="count">Количество.</param>
        /// <param name="lastFollowingId">Код последней подписки (для нужд постраничной выборки).</param>
        /// <param name="search">Поиск в ФИО и имени.</param>
        /// <returns>Список пользователей.</returns>
        Task<IQueryable<PortalUser>> GetFollowings(Guid userId, Guid? lastFollowingId, string search, int count);
    }
}
