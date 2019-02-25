using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.RemoveList
{
    /// <summary>
    /// Запрос на удаление списка.
    /// </summary>
    public class RemoveListRequest: MessageInputBase,IListIdentifiable
    {
        /// <summary>
        /// Код удаляемого списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
