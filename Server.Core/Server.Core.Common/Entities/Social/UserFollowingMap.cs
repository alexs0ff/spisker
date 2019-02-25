using System;

namespace Server.Core.Common.Entities.Social
{
    /// <summary>
    /// Сопостовления друзей следования пользователей друг за другом.
    /// </summary>
    public class UserFollowingMap : EntityBase<Guid>
    {
        public System.Guid UserFollowingMapID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid MasterID { get; set; }
        public Nullable<System.Guid> ChildID { get; set; }

        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public override Guid GetId()
        {
            return UserFollowingMapID;
        }
    }
}
