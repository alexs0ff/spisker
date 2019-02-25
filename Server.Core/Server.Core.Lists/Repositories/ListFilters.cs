using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Core.Common.Entities.Lists;

namespace Server.Core.Lists.Repositories
{
    public static class ListFilters
    {
        /// <summary>
        /// Фильтр на только опубликованные записи.
        /// </summary>
        /// <param name="lists">Список для фильтрации.</param>
        /// <param name="onlyPublished">Только опубликованные записи.</param>
        /// <returns></returns>
        public static IQueryable<List> PublishedFilter(this IQueryable<List> lists, bool onlyPublished)
        {
            if (onlyPublished)
            {
                return lists.Where(l => l.IsPublished);
            }

            return lists;
        }
    }
}
