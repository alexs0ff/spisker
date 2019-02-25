using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    /// <summary>
    /// Ответ с ошибками при регистрации.
    /// </summary>
    public class ErrorRegistrationResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ErrorRegistrationResponse(string message)
        {
            Errors.Add(new ErrorInfo(ErrorCodes.ErrorRegistration, "Ошибки при регистрации: " + message));
        }
    }
}