using System;
using Server.Core.Common.Messages;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    /// <summary>
    /// Ответ на регистрацию пользователя.
    /// </summary>
    public class AccountRegistredResponse:MessageOutputBase
    {
        /// <summary>
        /// Код созданного пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя созданного пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}