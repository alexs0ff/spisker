using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.UpdateListCheckItemKind
{
    /// <summary>
    /// Запрос на изменение типа выделения пунктов списка.
    /// </summary>
    public class UpdateListCheckItemKindRequest:MessageInputBase, IListIdentifiable
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Обновляемый код типа выделения пунктов списка.
        /// </summary>
        public int ListCheckItemKind { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
