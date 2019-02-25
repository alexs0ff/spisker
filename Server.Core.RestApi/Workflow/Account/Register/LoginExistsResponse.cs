using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    /// <summary>
    /// Ответ, то что логин пользователя уже существует.
    /// </summary>
    public class LoginExistsResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public LoginExistsResponse()
        {
            Errors.Add(new ErrorInfo(ErrorCodes.LoginExists, "Логин уже существует"));
        }
    }
}