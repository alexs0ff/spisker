using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.UpdateListKind
{
    /// <summary>
    /// Запрос на обновление типа списка.
    /// </summary>
    public class UpdateListKindRequest:MessageInputBase, IListIdentifiable
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Обновляемый код списка.
        /// </summary>
        public int ListKind { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
