using System;
using System.Collections.Generic;

namespace Server.Core.Lists.Models
{
    /// <summary>
    /// Модель списка.
    /// </summary>
    public class ListModel
    {
        /// <summary>
        /// Код списка модели.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название списка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Utc время и дата создания.
        /// </summary>
        public DateTime CreateEventTime { get; set; }

        /// <summary>
        /// Количество лайков.
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// Количество репостов.
        /// </summary>
        public int RepostCount { get; set; }

        /// <summary>
        /// Код владельца.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// ФИО владельца.
        /// </summary>
        public string OwnerFullName { get; set; }

        /// <summary>
        /// Логин владельца.
        /// </summary>
        public string OwnerLogin { get; set; }

        /// <summary>
        /// Логин создателя.
        /// </summary>
        public string OriginLogin { get; set; }

        /// <summary>
        /// ФИО создателя.
        /// </summary>
        public string OriginFullName { get; set; }

        /// <summary>
        /// Код создателя списка.
        /// </summary>
        public string OriginId { get; set; }

        /// <summary>
        /// Пункты списка.
        /// </summary>
        public List<ListItemModel> Items { get; set; }

        /// <summary>
        /// Текущий пользователь ставил лайк на этот список.
        /// </summary>
        public bool CurrentUserHasLike { get; set; }

        /// <summary>
        /// Тип списка.
        /// </summary>
        public int ListKind { get; set; }

        /// <summary>
        /// Типа выбора пунктов списка
        /// </summary>
        public int ListCheckItemKind { get; set; }

        /// <summary>
        /// Url для аватара.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Код списка.
        /// </summary>
        public long PublicId { get; set; }

        /// <summary>
        /// Признак опубликованного списка.
        /// </summary>
        public bool IsPublished { get; set; }
    }
}
