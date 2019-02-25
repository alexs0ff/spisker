using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.ChangePassword
{
    /// <summary>
    /// Ошибочный старый пароль.
    /// </summary>
    public class WrongOldPasswordResponse: MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WrongOldPasswordResponse()
        {
            Errors.Add(new ErrorInfo(ErrorCodes.WrongOldPassword, "Ошибочный старый пароль "));
        }
    }
}