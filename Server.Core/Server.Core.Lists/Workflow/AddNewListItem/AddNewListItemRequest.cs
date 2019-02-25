using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.AddNewListItem
{
    /// <summary>
    /// Запрос на добавление пункта списка.
    /// </summary>
    public class AddNewListItemRequest:MessageInputBase, IListIdentifiable
    {
        /// <summary>
        /// Код списка куда нужно добавить пункт.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Содержание пункта.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Код пункта списка после которого необходимо добавить пункт.
        /// </summary>
        public string AfterListItemId { get; set; }
    }
}
