using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;
using Server.Core.Lists.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.UpdateListItemCheck
{
    /// <summary>
    /// Параметры запроса на изменение состояния пункта списка.
    /// </summary>
    public class UpdateListItemCheckRequest:MessageInputBase, IListIdentifiable, IListItemIdentifiable
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Код пункта списка.
        /// </summary>
        public string ListItemId { get; set; }

        /// <summary>
        /// Обновляемое состояние пункта списка.
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
