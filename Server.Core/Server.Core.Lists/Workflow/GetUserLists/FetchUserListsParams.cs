using System;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Lists.Workflow.GetUserLists
{
    /// <summary>
    /// Параметры  для операции получения пользовательских списков.
    /// </summary>
    public class FetchUserListsParams: StepParameters
    {
        /// <summary>
        /// Пользователь которого нашли.
        /// </summary>
        public PortalUser User { get; set; }

        /// <summary>
        /// Имя пользователя на которого производится выборка списков.
        /// </summary>
        public string ForUserName { get; set; }

        /// <summary>
        /// Код пользователя на которого производится выборка списков.
        /// </summary>
        public Guid? ForUserId { get; set; }

        /// <summary>
        /// Последний идентификатор списка.
        /// </summary>
        public string LastListId { get; set; }

        /// <summary>
        /// Ответ на запросы по спискам пользователя.
        /// </summary>
        public GetUserListsResponse ListsResponse { get; set; }

        /// <summary>
        /// Публичный код списка, который должен быть первым (для случая когда LastListId = null).
        /// </summary>
        public long SelectedListNumber { get; set; }
    }
}
