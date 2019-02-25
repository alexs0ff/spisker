using System;
using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.GetUserLists
{
    /// <summary>
    /// Запрос на получение списка для конкретного пользователя.
    /// </summary>
    public class GetUserListsRequest: MessageInputBase
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя пользователя на которого производится выборка списков.
        /// </summary>
        public string ForUserName { get; set; }

        /// <summary>
        /// Код пользователя на которого производится выборка списков.
        /// </summary>
        public Guid? ForUserId { get; set; }

        /// <summary>
        /// Последний идентификатор.
        /// </summary>
        public string LastListId { get; set; }

        /// <summary>
        /// Публичный код списка, который должен быть первым (для случая когда LastListId = null).
        /// </summary>
        public string SelectedListNumber { get; set; }
    }
}
