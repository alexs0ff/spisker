using Server.Core.Common.Messages;

namespace Server.Core.Common.Workflow.CheckUserListExists
{

    /// <summary>
    /// Ответ то что пользовательский список не найден.
    /// </summary>
    public class UserListNotFoundResponse: MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UserListNotFoundResponse()
        {
            Errors.Add(new ErrorInfo(CommonErrors.UserListNotFound, "Список пользователя не найден"));
        }
    }
}
