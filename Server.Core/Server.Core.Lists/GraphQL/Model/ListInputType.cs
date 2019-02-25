using GraphQL.Types;

namespace Server.Core.Lists.GraphQL.Model
{
    public class ListInputType: InputObjectGraphType
    {
        public ListInputType()
        {
            Name = "ListInput";

            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
