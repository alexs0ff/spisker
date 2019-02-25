using System;
using System.Collections.Generic;
using System.Text;
using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.UpdatePublished
{
    /// <summary>
    /// Ответ по изменению типа публикации списка.
    /// </summary>
    public class UpdatePublishedResponse: MessageOutputBase
    {
        /// <summary>
        /// Код обновленного списка.
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Обновленный признак публикации списка.
        /// </summary>
        public bool IsPublished { get; set; }
    }
}
