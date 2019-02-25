using Server.Core.Common.Messages;
using Server.RestApi.Workflow;

namespace Server.Core.RestApi.Workflow.RecoverPassword
{
    /// <summary>
    /// Ответ с ошибочным номером проверки смены пароля.
    /// </summary>
    public class WrongNumberResponse : MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WrongNumberResponse()
        {
            Errors.Add(new ErrorInfo(ErrorCodes.WrongRecoverNumber, "Ошибочный номер восстановления"));
        }
    }
}