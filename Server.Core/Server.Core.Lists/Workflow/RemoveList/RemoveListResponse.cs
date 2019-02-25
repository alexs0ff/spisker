using System;
using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.RemoveList
{
    /// <summary>
    /// Ответ на удаление списка.
    /// </summary>
    public class RemoveListResponse: MessageOutputBase
    {
        /// <summary>
        /// Код удаленного списка.
        /// </summary>
        public Guid ListId { get; set; }
    }
}
