using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    public class WrongEmailResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WrongEmailResponse()
        {
            Errors.Add(new ErrorInfo(ErrorCodes.WrongEmail,"Ошибочный email")); 
        }
    }
}