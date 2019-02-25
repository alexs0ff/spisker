using GraphQL.Types;
using Server.Core.Common.GraphQL;
using Server.Core.Social.GraphQL.Model;

namespace Server.Core.Social.GraphQL
{
    /// <summary>
    /// Определитель схем по спискам.
    /// </summary>
    public class SocialSchemaDefiner : SchemaDefinerBase
    {
        /// <summary>
        /// Вызывается для инициализации схемы для запросов GraphQL.
        /// </summary>
        /// <param name="schema">Объект инициализируемой схемы.</param>
        public override void InitializeQuery(ObjectGraphType<object> schema)
        {
            schema.FieldAsync<ListGraphType<PortalUserProfileType >>("Users",
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>>
                        {
                            Name = "name",
                            Description = "Строка поиска"
                        }
                    ),
                    resolve:GetResolver<object, IGetUsersResolver>(),
                    description:"Поиск пользователей"
                );
        }

        /// <summary>
        /// Вызывается для инициализации схемы для мутаций GraphQL.
        /// </summary>
        /// <param name="schema">Объект инициализируемой схемы.</param>
        public override void InitializeMutation(ObjectGraphType<object> schema)
        {
            
        }
    }
}
