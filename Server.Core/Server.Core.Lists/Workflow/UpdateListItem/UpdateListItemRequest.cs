using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;
using Server.Core.Lists.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.UpdateListItem
{
    /// <summary>
    /// Запрос на обновление пункта списка.
    /// </summary>
    public class UpdateListItemRequest:MessageInputBase, IListIdentifiable, IListItemIdentifiable
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
        /// Обновляемое содержание.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
