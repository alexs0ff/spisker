using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.ChangePassword
{
    /// <summary>
    /// Общие ошибки при регистрации.
    /// </summary>
    public class ErrorChangePasswordResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ErrorChangePasswordResponse(string message)
        {
            Errors.Add(new ErrorInfo(ErrorCodes.ErrorChangePassword, "Ошибки при регистрации: " + message));
        }
    }
}