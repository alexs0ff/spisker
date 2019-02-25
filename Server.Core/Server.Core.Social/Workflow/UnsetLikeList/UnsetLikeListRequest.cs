using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Social.Workflow.UnsetLikeList
{
    /// <summary>
    /// Запрос по отзыву лайка.
    /// </summary>
    public class UnsetLikeListRequest:MessageInputBase, IListIdentifiable
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
