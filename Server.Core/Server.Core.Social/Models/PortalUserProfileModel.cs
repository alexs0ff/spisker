namespace Server.Core.Social.Models
{
    /// <summary>
    /// Модель профиля пользователя.
    /// </summary>
    public class PortalUserProfileModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string StatusText { get; set; }

        public string AvatarUrl { get; set; }

        public long ListCount { get; set; }

        public long FollowerCount { get; set; }

        public long FollowingCount { get; set; }

        /// <summary>
        /// Признак того, что текущий пользователь подписан на данного пользователя.
        /// </summary>
        public bool IsFollowing { get; set; }
    }
}
