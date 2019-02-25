using System;
using System.Globalization;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Server.Core.Common.Messages;

namespace Server.Core.Common.GraphQL
{
    /// <summary>
    /// Определитель схемы для объектов GrpaphQL в сборочных модулях.
    /// </summary>
    public abstract class SchemaDefinerBase
    {
        /// <summary>
        /// Вызывается для инициализации схемы для запросов GraphQL.
        /// </summary>
        /// <param name="schema">Объект инициализируемой схемы.</param>
        public abstract void InitializeQuery(ObjectGraphType<object> schema);

        /// <summary>
        /// Вызывается для инициализации схемы для мутаций GraphQL.
        /// </summary>
        /// <param name="schema">Объект инициализируемой схемы.</param>
        public abstract void InitializeMutation(ObjectGraphType<object> schema);


        private class ResolverData<T, TResolver, TContext>
            where TResolver : class, ITypeResolver<T>
            where TContext: ResolveFieldContext<T>
        {
            /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
            public ResolverData(bool needAuth)
            {
                _needAuth = needAuth;
            }

            /// <summary>
            /// Признак необходимости авторизации.
            /// </summary>
            private readonly bool _needAuth;

            public async Task<T> Resolve(TContext context)
            {
                var resolver = StartEnumServer.Instance.GetResolver<TResolver, T>();

                if (resolver == null)
                {
                    throw new Exception("Не определен тип резолвера");
                }

                if (_needAuth && !context.UserContext.As<UserContext>().IsAuthenticated)
                {
                    var errorCode = (int)CommonErrors.Unauthorized;
                    context.Errors.Add(new ValidationError(context.Document.OriginalQuery,
                        errorCode.ToString(CultureInfo.InvariantCulture), "Необходима авторизация", context.FieldAst));
                    return default(T);
                }

                var raw = await resolver.Resolve(context);
                return (T) raw;
            }
        }

        /// <summary>
        /// Получает функцию резолвинга.
        /// </summary>
        /// <typeparam name="T">Тип резолвинга.</typeparam>
        /// <typeparam name="TResolver">Тип резолвера.</typeparam>
        /// <param name="needAuth">Необходимость авторизованных запросов.</param>
        /// <returns>Функция резолвинга.</returns>
        protected Func<ResolveFieldContext<T>, Task<T>> GetResolver<T, TResolver>(bool needAuth = false)
            where TResolver:class,ITypeResolver<T>
            
        {

            var data = new ResolverData<T, TResolver, ResolveFieldContext<T>>(needAuth);

            return data.Resolve;
        }
    }
}
