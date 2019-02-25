using GraphQL.Types;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.GraphQL.Model
{
    /// <summary>
    /// GrpahQL тип списка.
    /// </summary>
    public class ListType: ObjectGraphType<ListModel>
    {
        public ListType()
        {
            Name = "List";
            Field(m => m.Id).Description("Код списка модели");
            Field(m => m.Name).Description("Название списка");
            Field(m => m.AvatarUrl).Description("Url к аватару");
            Field(m => m.CreateEventTime).Description("Дата и время создания пункта");
            Field(m => m.CurrentUserHasLike).Description("Признак наличия у текущего пользователя лайка у списка");
            Field(m => m.LikeCount).Description("Количество лайков");
            Field(m => m.ListCheckItemKind).Description("Тип выбора пунктов списка");
            Field(m => m.ListKind).Description("Тип списка");
            Field(m => m.OriginFullName).Description("ФИО создателя списка");
            Field(m => m.OriginId).Description("Код создателя списка");
            Field(m => m.OriginLogin).Description("Логин создателя списка");
            Field(m => m.OwnerFullName).Description("ФИО владельца списка");
            Field(m => m.OwnerId).Description("Код владельца списка");
            Field(m => m.OwnerLogin).Description("Логин владельца списка");
            Field(m => m.PublicId).Description("Публичный код списка");
            Field(m => m.RepostCount).Description("Количество репостов");
            Field<ListGraphType<ListItemType>>("Items", resolve: context => context.Source.Items);
        }
    }
}
