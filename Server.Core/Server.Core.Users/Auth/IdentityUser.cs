
using Server.Core.Common.Entities.Users;

namespace Server.Core.Users.Auth
{
    /// <summary>
    /// Пользователь для Asp.Identity.
    /// </summary>
    public class IdentityUser:PortalUser//, IUser
    {
        /// <summary>Unique key for the user</summary>
        public string Id => PortalUserID.ToString();
    }
}
