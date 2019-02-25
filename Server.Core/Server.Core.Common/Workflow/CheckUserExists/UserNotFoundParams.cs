namespace Server.Core.Common.Workflow.CheckUserExists
{

    /// <summary>
    /// Параметры не нахождения пользователя.
    /// </summary>
    public class UserNotFoundParams: StepParameters
    {
        /// <summary>
        /// Имя пользователя который был не найден.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Ответ то что пользователь не найден.
        /// </summary>
        public UserNotFoundResponse Response { get; set; }
    }
}
