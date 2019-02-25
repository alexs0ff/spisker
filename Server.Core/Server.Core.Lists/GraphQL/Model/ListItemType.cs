using GraphQL.Types;
using Server.Core.Lists.Models;

namespace Server.Core.Lists.GraphQL.Model
{
    /// <summary>
    /// GraphQL тип пункта списка.
    /// </summary>
    public class ListItemType: ObjectGraphType<ListItemModel>
    {
        public ListItemType()
        {
            Name = "ListItem";
            Field(m => m.Id).Description("Код пункта списка");
            Field(m => m.Content).Description("Содержание пункта");
            Field(m => m.CreateEventTime).Description("Дата и время создания UTC");
            Field(m => m.EditEventTime).Description("Дата редактирования UTC");
            Field(m => m.IsChecked).Description("Признак выбранного списка");
            Field(m => m.LikeCount).Description("Количество лайков");
            Field(m => m.OrderPosition).Description("Позиция в списке");
            Field(m => m.OwnerId).Description("Код владельца");
        }
    }
}
