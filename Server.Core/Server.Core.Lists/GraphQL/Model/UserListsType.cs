using GraphQL.Types;
using Server.Core.Lists.Workflow.GetUserLists;

namespace Server.Core.Lists.GraphQL.Model
{
    /// <summary>
    /// Тип ответа по спискам пользователей.
    /// </summary>
    public class UserListsType:ObjectGraphType<GetUserListsResponse>
    {
        public UserListsType()
        {
            Name = "UserLists";
            Field(m => m.FeedKind).Description("Тип ответа");
            Field(m=>m.SelectedListId,true,typeof(StringGraphType)).Description("Код выбранного списка");
            Field(m => m.LastListId,true, typeof(StringGraphType)).Description("Код последнего списка в текущей выборке");
            Field(m => m.HasMore).Description("Признак наличия других списков в последующей выборке");
            Field<ListGraphType<ListType>>("Lists", resolve: context => context.Source.Lists);
        }
    }
}
