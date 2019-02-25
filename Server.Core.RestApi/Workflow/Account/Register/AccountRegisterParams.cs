using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    /// <summary>
    /// Параметры регистрации учетной записи.
    /// </summary>
    public class AccountRegisterParams: StepParameters
    {
        /// <summary>
        /// Запрос на регистрацию.
        /// </summary>
        public AccountRegisterRequest Request { get; set; }

        /// <summary>
        /// Ответ на запрос.
        /// </summary>
        public MessageOutputBase Response { get; set; }
    }
}