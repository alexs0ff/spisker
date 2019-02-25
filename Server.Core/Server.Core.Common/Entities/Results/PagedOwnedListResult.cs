using System;
using System.Linq;
using Server.Core.Common.Entities.Lists;

namespace Server.Core.Common.Entities.Results
{
    /// <summary>
    /// Результат пейджированной страницы со списками.
    /// </summary>
    public class PagedOwnedListResult
    {
        /// <summary>
        /// Последний код списка.
        /// </summary>
        public Guid? LastListId { get; set; }

        /// <summary>
        /// Признак наличия возмоности получить следующие списки/
        /// </summary>
        public bool HasMore { get; set; }


        /// <summary>
        /// Результирующие списки.
        /// </summary>
        public IQueryable<OwnedList> OwnedLists { get; set; }
    }
}
