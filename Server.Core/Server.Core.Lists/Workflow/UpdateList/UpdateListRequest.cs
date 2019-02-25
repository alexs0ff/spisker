using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.UpdateList
{
    /// <summary>
    /// Запрос обновления списка.
    /// </summary>
    public class UpdateListRequest:MessageInputBase, IListIdentifiable
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Обновляемое имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
