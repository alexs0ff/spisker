using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Social;

namespace Server.Core.Common.Repositories.Social
{
    /// <summary>
    /// Репозиторий лайков пользователя.
    /// </summary>
    public interface IListUserLikeMapRepository : ICrudRepository<ListUserLikeMap, Guid>
    {
        /// <summary>
        /// Признак что список имеет лайк.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Признак наличия.</returns>
        Task<bool> UserListHasLike(Guid listId, Guid userId);

        /// <summary>
        /// Установка лайка списку.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Код установленного лайка.</returns>
        Task<Guid> SetLike(Guid listId, Guid userId);

        /// <summary>
        /// Удаление лайка из списка.
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <param name="userId">Код пользователя.</param>
        Task UnsetLike(Guid listId, Guid userId);

        /// <summary>
        /// Заполняет списки лайками пользователя.
        /// </summary>
        /// <param name="lists">Списко.</param>
        /// <param name="userId">Пользователь на проверку наличия лайков.</param>
        Task FillUserLikes(List<OwnedList> lists, Guid userId);

        /// <summary>
        /// Заполняет списки лайками пользователя.
        /// </summary>
        /// <param name="lists">Списко.</param>
        /// <param name="userName">Пользователь на проверку наличия лайков.</param>
        Task FillUserLikes(List<OwnedList> lists, string userName);

        /// <summary>
        /// Получает количество лайков у списка и 
        /// </summary>
        /// <param name="listId">Код списка.</param>
        /// <returns>Количество лайков.</returns>
        Task<long> GetLikeCount(Guid listId);
    }
}
