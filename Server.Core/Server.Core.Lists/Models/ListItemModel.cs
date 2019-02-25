using System;

namespace Server.Core.Lists.Models
{
    /// <summary>
    /// Модель пункта списка.
    /// </summary>
    public class ListItemModel
    {
        /// <summary>
        /// Код пункта списка.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Содержание списка.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Позиция в пункте меню.
        /// </summary>
        public int OrderPosition { get; set; }

        /// <summary>
        /// Дата создания UTC.
        /// </summary>
        public DateTime CreateEventTime { get; set; }

        /// <summary>
        /// Дата редактирования UTC.
        /// </summary>
        public DateTime EditEventTime { get; set; }

        /// <summary>
        /// Количество лайков.
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// Код владельца.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// Признак выбранного пункта.
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
