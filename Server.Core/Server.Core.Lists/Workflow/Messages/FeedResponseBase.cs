using System;
using System.Collections.Generic;
using Server.Core.Common.Messages;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.Workflow.Messages
{
    /// <summary>
    /// Базовый класс для получения списков пользователей.
    /// </summary>
    public class FeedResponseBase:MessageOutputBase
    {
        /// <summary>
        /// Тип ответа.
        /// </summary>
        public int FeedKind { get; set; }

        /// <summary>
        /// Списки пользователей.
        /// </summary>
        public List<ListModel> Lists { get; set; }

        /// <summary>
        /// Код последнего списка.
        /// </summary>
        public Guid? LastListId { get; set; }

        /// <summary>
        /// Признак возможности придти за другими списками.
        /// </summary>
        public bool HasMore { get; set; }
        
    }
}
