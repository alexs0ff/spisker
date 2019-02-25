using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    /// <summary>
    /// Сообщение что пароль не прошел по параметрам.
    /// </summary>
    public class WrongPasswordResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WrongPasswordResponse(string message)
        {
            Errors.Add(new ErrorInfo(ErrorCodes.WrongPassword,"Ошибочный пароль "+ message));
        }
    }
}