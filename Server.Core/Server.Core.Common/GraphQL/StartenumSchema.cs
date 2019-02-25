using GraphQL.Types;

namespace Server.Core.Common.GraphQL
{
    /// <summary>
    /// Общая схема для 
    /// </summary>
    public class StartenumSchema:Schema
    {
        public StartenumSchema()
        {
            
        }

        internal StartenumSchema(ObjectGraphType<object> query, ObjectGraphType<object> mutation)
        {
            Query = query;
            Mutation = mutation;
        }
    }
}
