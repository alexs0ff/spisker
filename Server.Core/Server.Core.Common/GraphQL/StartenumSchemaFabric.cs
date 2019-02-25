using GraphQL.Types;

namespace Server.Core.Common.GraphQL
{
    /// <summary>
    /// Фабрика для регистрации схемы.
    /// </summary>
    public class StartenumSchemaFabric
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        private StartenumSchemaFabric()
        {
            _query.Name = "Query";
            _mutation.Name = "Mutation";
        }

        /// <summary>
        /// Запросы.
        /// </summary>
        readonly ObjectGraphType<object> _query = new ObjectGraphType<object>();

        /// <summary>
        /// Мутации.
        /// </summary>
        readonly ObjectGraphType<object> _mutation = new ObjectGraphType<object>();

        /// <summary>
        /// Создание фабрики регистрации схемы GraphQL.
        /// </summary>
        /// <returns>Созданная фабрика.</returns>
        public static StartenumSchemaFabric Create()
        {
            return new StartenumSchemaFabric();
        }

        /// <summary>
        /// Регистрация модульного определителя схемы.
        /// </summary>
        /// <param name="definer">Определитель.</param>
        /// <returns>Фабрика.</returns>
        public StartenumSchemaFabric Register(SchemaDefinerBase definer)
        {
            definer.InitializeQuery(_query);
            definer.InitializeMutation(_mutation);

            return this;
        }

        /// <summary>
        /// Финальное создание схемы.
        /// </summary>
        /// <returns>Созданная схема.</returns>
        public StartenumSchema Done()
        {
            return new StartenumSchema(_query, _mutation);
        }
    }
}
