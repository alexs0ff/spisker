namespace Server.Core.Common.Workflow.CheckListExists
{
    /// <summary>
    /// Параметры списка.
    /// </summary>
    public class ListNotFoundParams: StepParameters
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Ответ то что список не найден.
        /// </summary>
        public ListNotFoundResponse Response { get; set; }
    }
}
