using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Social;

namespace Server.Core.Common.Repositories.Social
{
    /// <summary>
    /// Репозиторий профилей пользователей.
    /// </summary>
    public interface IPortalUserProfileRespository : ICrudRepository<PortalUserProfile, Guid>
    {
        /// <summary>
        /// Получает профиль пользователя по коду пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Найденный профиль или null.</returns>
        Task<PortalUserProfile> GetByUserId(Guid userId);

        /// <summary>
        /// Повышает на единицу количество списко в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        Task IncrementListCount(Guid userId);

        /// <summary>
        /// Уменьшает на единицу количество списко в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        Task DecrementListCount(Guid userId);

        /// <summary>
        /// Повышает на единицу количество следователей в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        Task IncrementFollowerCount(Guid userId);

        /// <summary>
        /// Уменьшает на единицу количество следователей в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        Task DecrementFollowerCount(Guid userId);

        /// <summary>
        /// Повышает на единицу количество преследуемых в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        Task IncrementFollowingCount(Guid userId);

        /// <summary>
        /// Уменьшает на единицу количество преследуемых в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        Task DecrementFollowingCount(Guid userId);

        /// <summary>
        /// Изменение статуса.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="newStatus">Новый статус.</param>
        Task ChangeStatus(Guid userId, string newStatus);

        /// <summary>
        /// Изменение аватара пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="newAvatarId">Новый аватар.</param>
        Task ChangeAvatar(Guid userId, Guid newAvatarId);

        /// <summary>
        /// Заполняет списки урлами к аватарам пользователей.
        /// </summary>
        /// <param name="lists">Спискb.</param>
        Task FillAvatars(List<OwnedList> lists);

        /// <summary>
        /// Заполняет список урлами к аватару пользователей.
        /// </summary>
        /// <param name="list">Список.</param>
        Task FillAvatar(OwnedList list);

        /// <summary>
        /// Получает код аватара пользователя.
        /// </summary>
        /// <param name="userId">КОд пользователя.</param>
        /// <returns>КОд аватара или null.</returns>
        Task<Guid?> GetAvatarId(Guid userId);
    }
}
