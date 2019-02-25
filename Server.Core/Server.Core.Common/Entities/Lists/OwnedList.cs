namespace Server.Core.Common.Entities.Lists
{
    /// <summary>
    /// Список с владельцем.
    /// </summary>
    public class OwnedList:List
    {
        /// <summary>
        /// Имя пользователя владельца.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя пользователя владельца.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя владельца.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользователя владельца.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Текущий пользователь ставил лайк на этот список.
        /// </summary>
        public bool CurrentUserHasLike { get; set; }

        /// <summary>
        /// Url для аватара пользователя.
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
