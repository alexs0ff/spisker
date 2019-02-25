using Server.Core.Common.Messages;

namespace Server.Core.Common.Workflow.CheckRecaptcha
{
    /// <summary>
    /// Сообщение с нверной рекапчей.
    /// </summary>
    public class InvalidRecaptchaResponse: MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public InvalidRecaptchaResponse()
        {
            Errors.Add(new ErrorInfo(CommonErrors.InvalidRecaptcha, "Ошибочная рекапча"));
        }
    }
}
