using GraphQL.Types;
using Server.Core.Common.GraphQL;
using Server.Core.Lists.GraphQL.Model;

namespace Server.Core.Lists.GraphQL
{
    /// <summary>
    /// Определитель схем по спискам.
    /// </summary>
    public class ListsSchemaDefiner: SchemaDefinerBase
    {
        /// <summary>
        /// Вызывается для инициализации схемы для запросов GraphQL.
        /// </summary>
        /// <param name="schema">Объект инициализируемой схемы.</param>
        public override void InitializeQuery(ObjectGraphType<object> schema)
        {
            schema.FieldAsync<UserListsType>("UserLists",
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>>
                        {
                            Name = "userName",
                            Description = "Имя пользователя"
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "lastListId",
                            Description = "Последний идентификатор в предыдущей выборке списков"
                        },
                        new QueryArgument<StringGraphType>()
                        {
                            Name = "selectedListNumber",
                            Description = "Публичный код списка, который должен быть первым (для случая когда LastListId = null)"
                        }
                    ),
                    resolve:GetResolver<object,IListResolver>(),
                    description:"Получение списков пользователя"

                );
        }

        /// <summary>
        /// Вызывается для инициализации схемы для мутаций GraphQL.
        /// </summary>
        /// <param name="schema">Объект инициализируемой схемы.</param>
        public override void InitializeMutation(ObjectGraphType<object> schema)
        {
            schema.FieldAsync<ListType>(
                "createList",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListInputType>> { Name = "list" }
                ),
                resolve: GetResolver<object, IAddNewListResolver>(true),
                description:"Создание нового списка"
                );
        }
    }
}
