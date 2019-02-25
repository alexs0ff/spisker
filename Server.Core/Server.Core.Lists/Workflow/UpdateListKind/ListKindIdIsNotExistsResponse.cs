using Server.Core.Common.Messages;
using Server.Core.Lists.Messages;

namespace Server.Core.Lists.Workflow.UpdateListKind
{
    /// <summary>
    /// Ответ, что тип списка не найден.
    /// </summary>
    public class ListKindIdIsNotExistsResponse:MessageOutputBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ListKindIdIsNotExistsResponse(int kindId)
        {
            Errors.Add(new ErrorInfo(ErrorCodes.ListKindIdIsNotExists,$"Код типа списка {kindId} не существует"));
        }
    }
}
