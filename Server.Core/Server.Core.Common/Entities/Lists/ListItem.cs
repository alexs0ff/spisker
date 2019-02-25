using System;

namespace Server.Core.Common.Entities.Lists
{
    public class ListItem:EntityBase<Guid>
    {
        public System.Guid ListItemID { get; set; }
        public string Content { get; set; }
        public int OrderPosition { get; set; }
        public System.DateTime CreateEventTimeUTC { get; set; }
        public System.DateTime EditEventTimeUTC { get; set; }
        public long LikeCount { get; set; }
        public System.Guid ListID { get; set; }
        public bool IsChecked { get; set; }
        public System.Guid PortalUserID { get; set; }

        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public override Guid GetId()
        {
            return ListItemID;
        }
    }
}
