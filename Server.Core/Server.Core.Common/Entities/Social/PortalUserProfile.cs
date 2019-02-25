using System;

namespace Server.Core.Common.Entities.Social
{
    /// <summary>
    /// Профиль пользователя.
    /// </summary>
    public class PortalUserProfile : EntityBase<Guid>
    {
        public System.Guid PortalUserProfileID { get; set; }
        public string StatusText { get; set; }
        public Nullable<System.Guid> AvatarID { get; set; }
        public long ListCount { get; set; }
        public long FollowerCount { get; set; }
        public long FollowingCount { get; set; }
        public System.Guid PortalUserID { get; set; }

        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public override Guid GetId()
        {
            return PortalUserProfileID;
        }
    }
}
