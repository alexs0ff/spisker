using System;
using System.Linq;
using Server.Core.Common.Entities;

namespace Server.Core.Common.Repositories.Filters
{
    /// <summary>
    /// Хэлпер для фильтрации сущностей.
    /// </summary>
    public static class FilterPipe
    {
        /// <summary>
        /// Производит вызов функции фильтра для определенных сущностей.
        /// </summary>
        /// <returns>Результат.</returns>
        public static IQueryable<T> Filter<T>(this IQueryable<T> entities,Func<IQueryable<T>, IQueryable<T>> filterFunc)
            where T:EntityBase<Guid>

        {
            return filterFunc(entities);
        }
    }
}
