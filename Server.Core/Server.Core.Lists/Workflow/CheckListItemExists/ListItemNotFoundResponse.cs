using Server.Core.Common.Messages;
using Server.Core.Lists.Messages;

namespace Server.Core.Lists.Workflow.CheckListItemExists
{
    /// <summary>
    /// Ответ, что список не найден.
    /// </summary>
    public class ListItemNotFoundResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ListItemNotFoundResponse()
        {
            Errors.Add(new ErrorInfo(ErrorCodes.ListItemNotFound, "Пункт списка не найден"));
        }
    }
}
