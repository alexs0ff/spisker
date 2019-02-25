using System;
using System.Collections.Generic;

namespace Server.Core.Common.GraphQL
{
    /// <summary>
    /// Пользовательский контекст.
    /// </summary>
    public class UserContext
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UserContext(string ip, bool isAuthenticated, string userName, Guid? userId, IEnumerable<System.Security.Claims.Claim> claims)
        {
            Ip = ip;
            IsAuthenticated = isAuthenticated;
            UserName = userName;
            UserId = userId;

            Claims = claims;

            if (Claims == null)
            {
                Claims = new List<System.Security.Claims.Claim>();
            }

            if (UserId.HasValue && UserId.Value == Guid.Empty)
            {
                UserId = null;
            }

            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserName = null;
            }
        }

        /// <summary>
        /// IP клиента.
        /// </summary>
        public string Ip { get; }

        /// <summary>
        /// Признак авторизации.
        /// </summary>
        public bool IsAuthenticated { get; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Код авторизированного пользователя.
        /// </summary>
        public Guid? UserId { get; }

        /// <summary>
        /// Текущие права пользователя.
        /// </summary>
        public IEnumerable<System.Security.Claims.Claim> Claims { get; }
    }
}
