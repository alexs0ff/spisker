using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Core.Common.Entities.Lists
{
    public class List:EntityBase<Guid>
    {
        public List()
        {
            ListItem = new HashSet<ListItem>();
        }

        public System.Guid ListID { get; set; }
        public System.DateTime CreateEventTimeUTC { get; set; }
        public string Name { get; set; }
        public long LikeCount { get; set; }
        public long RepostCount { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PublicID { get; set; }
        public Guid? PortalUserID { get; set; }
        public Guid? OriginalListID { get; set; }
        public Guid? OriginalPortalUserID { get; set; }
        public string OriginUserName { get; set; }
        public string OriginFirstName { get; set; }
        public string OriginLastName { get; set; }
        public string OriginMiddleName { get; set; }
        public int ListKindID { get; set; }
        public int ListCheckItemKindID { get; set; }
        
        public virtual ICollection<ListItem> ListItem { get; set; }

        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public override Guid GetId()
        {
            return ListID;
        }
    }
}
