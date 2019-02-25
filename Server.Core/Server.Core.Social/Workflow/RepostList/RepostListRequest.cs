using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Social.Workflow.RepostList
{
    /// <summary>
    /// Запрос на репост списка.
    /// </summary>
    public class RepostListRequest: MessageInputBase,IListIdentifiable
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Имя пользователя который делает репост.
        /// </summary>
        public string UserName { get; set; }
    }
}
