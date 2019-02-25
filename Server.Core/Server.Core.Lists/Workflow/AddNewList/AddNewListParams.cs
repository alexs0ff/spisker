using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.AddNewList
{
    /// <summary>
    /// Параметры для операции добавления нового списка.
    /// </summary>
    public class AddNewListParams : StepParameters
    {
        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Название списка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ответ на добавление нового списка.
        /// </summary>
        public AddNewListResponse Response { get; set; }
    }
}
