using System;
using System.Collections.Generic;
using Server.Core.Common.Entities.Social;

namespace Server.Core.Common.Entities.Users
{
    public class PortalUser:EntityBase<Guid>
    {
        public PortalUser()
        {
            UserRole = new HashSet<UserRole>();
            PortalUserProfile = new HashSet<PortalUserProfile>();
            UserFollowingMap = new HashSet<UserFollowingMap>();
            UserFollowingMap1 = new HashSet<UserFollowingMap>();
        }
        public System.Guid PortalUserID { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
        
        public virtual ICollection<PortalUserProfile> PortalUserProfile { get; set; }
        
        public virtual ICollection<UserFollowingMap> UserFollowingMap { get; set; }
        
        public virtual ICollection<UserFollowingMap> UserFollowingMap1 { get; set; }

        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public override Guid GetId()
        {
            return PortalUserID;
        }
    }
}
