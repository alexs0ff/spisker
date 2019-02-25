using Server.Core.Common.Messages;

namespace Server.Core.Common.Workflow.CheckRecaptcha
{
    public class CheckRecaptchaParams: StepParameters
    {
        /// <summary>
        /// Проверяемый токен рекапчи.
        /// </summary>
        public string RecaptchaToken { get; set; }

        /// <summary>
        /// Входящее сообщение.
        /// </summary>
        public MessageInputBase InputMessage { get; set; }

        /// <summary>
        /// Ответ.
        /// </summary>
        public MessageOutputBase Response { get; set; }
    }
}
