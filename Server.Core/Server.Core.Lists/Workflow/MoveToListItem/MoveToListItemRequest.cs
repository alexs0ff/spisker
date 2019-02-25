using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.MoveToListItem
{
    /// <summary>
    /// Запрос на перемещение пункта списка в другой пункт.
    /// </summary>
    public class MoveToListItemRequest:MessageInputBase
    {
        /// <summary>
        /// Список в который нужно переместить пункт может быть Null.
        /// </summary>
        public string TargetListId { get; set; }

        /// <summary>
        /// Код пункта списка который необходимо перренести.
        /// </summary>
        public string ListItemId { get; set; }

        /// <summary>
        /// Код пункта списка за который необходимо перенести.
        /// </summary>
        public string AfterListItemId { get; set; }

        /// <summary>
        /// Признак необходимости скопировать пункт.
        /// </summary>
        public bool Copy { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
