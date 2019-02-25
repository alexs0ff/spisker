using Server.Core.Common.Messages;
using Server.Core.Lists.Messages;

namespace Server.Core.Lists.Workflow.UpdateListCheckItemKind
{
    /// <summary>
    /// Ответ по не найденному типу выделения пункта списка.
    /// </summary>
    public class ListCheckItemKindIsIsNotExistsResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ListCheckItemKindIsIsNotExistsResponse(int checkKindId)
        {
            Errors.Add(new ErrorInfo(ErrorCodes.ListCheckItemKindIsIsNotExists, $"Код типа выделения пунктов списка {checkKindId} не существует"));
        }
    }
}
