using Server.Core.Common.Messages;

namespace Server.Core.Common.Workflow.CheckListExists
{
    /// <summary>
    /// Ответ, то что список не найден.
    /// </summary>
    public class ListNotFoundResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ListNotFoundResponse()
        {
            Errors.Add(new ErrorInfo(CommonErrors.ListNotFound, "Список не найден"));
        }
    }
}
