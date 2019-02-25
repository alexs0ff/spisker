using System;
using System.Collections.Generic;
using System.Text;
using Server.Core.Common.Messages;
using Server.Core.Common.Messages.Identifiable;

namespace Server.Core.Lists.Workflow.UpdatePublished
{
    /// <summary>
    /// Запрос на обновление признака публикации списка.
    /// </summary>
    public class UpdatePublishedRequest:MessageInputBase, IListIdentifiable
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Обновляемый признак публикации списка.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
