using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Social.Workflow.SetLikeList
{
    /// <summary>
    /// Запрос на установку лайка списка.
    /// </summary>
    public class SetLikeListRequest: MessageInputBase, IListIdentifiable
    {
        /// <summary>
        /// Имя пользователя от которого ставится лайк.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Код списка на который ставится лайк.
        /// </summary>
        public string ListId { get; set; }
    }
}
