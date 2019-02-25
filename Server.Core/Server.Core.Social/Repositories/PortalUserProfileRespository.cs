using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common;
using Server.Core.Common.Contexts;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Social;
using Server.Core.Common.Repositories;
using Server.Core.Common.Repositories.Files;
using Server.Core.Common.Repositories.Social;

namespace Server.Core.Social.Repositories
{
    /// <summary>
    /// Репозиторий для профилей пользователей.
    /// </summary>
    public class PortalUserProfileRespository : CrudRepositoryBase<PortalUserProfile, Guid, StartenumEntities,StartenumEntitiesFactory>,
        IPortalUserProfileRespository
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public PortalUserProfileRespository(StartenumEntitiesFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Получает профиль пользователя по коду пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Найденный профиль или null.</returns>
        public Task<PortalUserProfile> GetByUserId(Guid userId)
        {
            return Entities.FirstOrDefaultAsync(i => i.PortalUserID == userId);
        }

        #region List Count

        private Task AddListCount(Guid userId, int number)
        {
            const string sql = @"
            UPDATE [dbo].[PortalUserProfile]
            SET [ListCount] = ListCount + {0}    
            WHERE PortalUserID = {1}";
            return GetContext().Database.ExecuteSqlCommandAsync(sql, number, userId);
        }

        /// <summary>
        /// Повышает на единицу количество списко в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        public Task IncrementListCount(Guid userId)
        {
            return AddListCount(userId, 1);
        }

        /// <summary>
        /// Уменьшает на единицу количество списко в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        public Task DecrementListCount(Guid userId)
        {
            return AddListCount(userId, -1);
        }

        #endregion

        #region Follower Count

        private Task AddFollowerCount(Guid userId, int number)
        {
            const string sql = @"
            UPDATE [dbo].[PortalUserProfile]
            SET [FollowerCount] = FollowerCount + {0}    
            WHERE PortalUserID = {1}";
            return GetContext().Database.ExecuteSqlCommandAsync(sql, number, userId);
        }

        /// <summary>
        /// Повышает на единицу количество следователей в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        public Task IncrementFollowerCount(Guid userId)
        {
            return AddFollowerCount(userId, 1);
        }

        /// <summary>
        /// Уменьшает на единицу количество следователей в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        public Task DecrementFollowerCount(Guid userId)
        {
            return AddFollowerCount(userId, -1);
        }

        #endregion

        #region Following Count

        private Task AddFollowingCount(Guid userId, int number)
        {
            const string sql = @"
            UPDATE [dbo].[PortalUserProfile]
            SET [FollowingCount] = FollowingCount + {0}    
            WHERE PortalUserID = {1}";
            return GetContext().Database.ExecuteSqlCommandAsync(sql, number, userId);
        }

        /// <summary>
        /// Повышает на единицу количество преследуемых в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        public Task IncrementFollowingCount(Guid userId)
        {
            return AddFollowingCount(userId, 1);
        }

        /// <summary>
        /// Уменьшает на единицу количество преследуемых в профиле пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        public Task DecrementFollowingCount(Guid userId)
        {
            return AddFollowingCount(userId, -1);
        }

        #endregion

        /// <summary>
        /// Изменение статуса.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="newStatus">Новый статус.</param>
        public Task ChangeStatus(Guid userId, string newStatus)
        {
            const string sql = @"
            UPDATE [dbo].[PortalUserProfile]
            SET 
                [StatusText] = {1}
            WHERE 
                PortalUserID =  {0}";
            return GetContext().Database.ExecuteSqlCommandAsync(sql, userId, newStatus);
        }


        /// <summary>
        /// Изменение аватара пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="newAvatarId">Новый аватар.</param>
        public Task ChangeAvatar(Guid userId, Guid newAvatarId)
        {
            const string sql = @"
            UPDATE [dbo].[PortalUserProfile]
            SET 
                [AvatarID] = {1}
            WHERE 
                PortalUserID =  {0}";
            return GetContext().Database.ExecuteSqlCommandAsync(sql, userId, newAvatarId);
        }

        /// <summary>
        /// Заполняет список урлами к аватару пользователей.
        /// </summary>
        /// <param name="list">Список.</param>
        public Task FillAvatar(OwnedList list)
        {
            return FillAvatars(new List<OwnedList> {list});
        }

        /// <summary>
        /// Заполняет списки урлами к аватарам пользователей.
        /// </summary>
        /// <param name="lists">Спискb.</param>
        public async Task FillAvatars(List<OwnedList> lists)
        {
            var users = new Dictionary<Guid, string>();
            var fileRepository = StartEnumServer.Instance.GetRepository<IStoredFileRepository>();

            foreach (var ownedList in lists)
            {
                var id = ownedList.PortalUserID ?? Guid.Empty;
                if (users.ContainsKey(id))
                {
                    ownedList.AvatarUrl = users[id];
                }
                else
                {
                    var avatarId = await Entities.Where(i => i.PortalUserID == id)
                        .Select(i => i.AvatarID)
                        .FirstOrDefaultAsync();
                    if (avatarId != null)
                    {
                        var file = await fileRepository.GetFileUrl(avatarId.Value);
                        users[id] = file;
                        ownedList.AvatarUrl = file;
                    }
                    else
                    {
                        users[id] = null;
                    }
                }

            }
        }

        /// <summary>
        /// Получает код аватара пользователя.
        /// </summary>
        /// <param name="userId">КОд пользователя.</param>
        /// <returns>КОд аватара или null.</returns>
        public Task<Guid?> GetAvatarId(Guid userId)
        {
            return Entities.Where(i => i.PortalUserID == userId).Select(i => i.AvatarID).FirstOrDefaultAsync();
        }
    }
}
