using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    /// <summary>
    /// Ответ о том, что email уже существует.
    /// </summary>
    public class EmailExistsResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EmailExistsResponse()
        {
            Errors.Add(new ErrorInfo(ErrorCodes.EmailExists, "Email уже существует"));
        }
    }
}