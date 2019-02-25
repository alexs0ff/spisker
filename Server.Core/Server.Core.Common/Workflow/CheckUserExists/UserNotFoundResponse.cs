using Server.Core.Common.Messages;

namespace Server.Core.Common.Workflow.CheckUserExists
{
    /// <summary>
    /// Ответ то что пользователь не найден.
    /// </summary>
    public class UserNotFoundResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UserNotFoundResponse()
        {
            Errors.Add(new ErrorInfo(CommonErrors.UserNotFound,"Пользователь не найден"));
        }
    }
}
