using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Common.Entities.Social
{
    public class ListItemUserLikeMap
    {
        public System.Guid ListItemUserLikeMapID { get; set; }
        public System.DateTime CreateEventTimeUTC { get; set; }
        public System.Guid ListItemID { get; set; }
        public Nullable<System.Guid> PortalUserID { get; set; }
    }
}
