using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    public class WrongLoginResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WrongLoginResponse(string message)
        {
            Errors.Add(new ErrorInfo(ErrorCodes.WrongLogin, "Ошибочный логин: "+ message));
        }
    }
}