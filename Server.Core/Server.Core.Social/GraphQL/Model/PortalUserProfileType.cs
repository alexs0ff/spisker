using GraphQL.Types;
using Server.Core.Social.Models;

namespace Server.Core.Social.GraphQL.Model
{
    /// <summary>
    /// GraphQL тип для профилей.
    /// </summary>
    public class PortalUserProfileType: ObjectGraphType<PortalUserProfileModel>
    {
        public PortalUserProfileType()
        {
            Name = "UserProfile";
            Field(m => m.UserName).Description("Логин пользователя");
            Field(m => m.AvatarUrl,true).Description("Url аватара");
            Field(m => m.FirstName, true).Description("Имя пользователя");
            Field(m => m.FollowerCount).Description("Количество подписчиков");
            Field(m => m.FollowingCount).Description("Количество подписок");
            Field(m => m.LastName, true).Description("Фамилия пользователя");
            Field(m => m.MiddleName, true).Description("Отчество пользователя");
            Field(m => m.ListCount).Description("Количество списков");
            Field(m => m.StatusText, true).Description("Статус");
        }
    }
}
