using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.CheckListItemExists
{
    /// <summary>
    /// Параметры для определения не найденного пункта списка.
    /// </summary>
    public class ListItemNotFoundParams: StepParameters
    {
        /// <summary>
        /// Код пункт списка.
        /// </summary>
        public string ListItemId { get; set; }

        /// <summary>
        /// Ответ то что пункт списка не найден.
        /// </summary>
        public ListItemNotFoundResponse Response { get; set; }
    }
}
