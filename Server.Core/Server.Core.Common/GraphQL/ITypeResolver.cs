using System.Threading.Tasks;
using GraphQL.Types;

namespace Server.Core.Common.GraphQL
{
    /// <summary>
    /// Интерфейс резолверов типа.
    /// </summary>
    public interface ITypeResolver<TSourceType>
    {
        /// <summary>
        /// Производит резолвинг данных с текущим контекстом.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <returns>Данные.</returns>
        Task<object> Resolve(ResolveFieldContext<TSourceType> context);
    }
}
