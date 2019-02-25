using System;

namespace Server.Core.Common.Entities.Social
{
    public class ListUserLikeMap : EntityBase<Guid>
    {
        public System.Guid ListUserLikeMapID { get; set; }
        public System.DateTime CreateEventTimeUTC { get; set; }
        public System.Guid ListID { get; set; }
        public Nullable<System.Guid> PortalUserID { get; set; }

        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public override Guid GetId()
        {
            return ListUserLikeMapID;
        }
    }
}
