using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.MoveToListItem
{
    /// <summary>
    /// Параметры для шага по перемещению пункта списка.
    /// </summary>
    public class MoveToListItemParams: StepParameters
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
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Ответ по перемещению пункта списка.
        /// </summary>
        public MoveToListItemResponse Response { get; set; }

        /// <summary>
        /// Код пункта который был не найден.
        /// </summary>
        public string IdNotFound { get; set; }
    }
}
