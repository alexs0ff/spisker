using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;
using Server.Core.Lists.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.RemoveListItem
{
    /// <summary>
    /// Запрос на удаление пункта списка.
    /// </summary>
    public class RemoveListItemRequest: MessageInputBase,IListIdentifiable,IListItemIdentifiable
    {
        /// <summary>
        /// Код списка содержащего удаляемый пункт.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Код удаляемого пункта.
        /// </summary>
        public string ListItemId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
