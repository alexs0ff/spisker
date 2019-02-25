using Server.Core.Common.Messages;

namespace Server.Core.Lists.Workflow.AddNewList
{
    /// <summary>
    /// Запрос на создание нового списка.
    /// </summary>
    public class AddNewListRequest:MessageInputBase
    {
        /// <summary>
        /// Имя списка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя пользователя. Проверяется системой.
        /// </summary>
        public string UserName { get; set; }
    }
}
