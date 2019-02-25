using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.ChangePassword
{
    public class WrongNewPasswordResponse: MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WrongNewPasswordResponse(string message)
        {
            Errors.Add(new ErrorInfo(ErrorCodes.WrongNewPassword, "Ошибочный пароль " + message));
        }
    }
}