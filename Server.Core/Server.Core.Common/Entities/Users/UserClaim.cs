using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Common.Entities.Users
{
    public class UserClaim
    {
        public int UserClaimId { get; set; }
        public System.Guid UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
