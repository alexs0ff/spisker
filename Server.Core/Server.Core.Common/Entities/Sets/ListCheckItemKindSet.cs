using System.Collections.Generic;
using System.Linq;
using Server.Core.Common.Entities.Lists;

namespace Server.Core.Common.Entities.Sets
{
    /// <summary>
    /// Справочник по типам выделения пунктов списка.
    /// </summary>
    public static class ListCheckItemKindSet
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        static ListCheckItemKindSet()
        {
            None = new ListCheckItemKind{ListCheckItemKindID = 1,Title = "Без выделения"};
            Simple = new ListCheckItemKind{ListCheckItemKindID = 2,Title = "Простой"};
            LineThrough = new ListCheckItemKind{ListCheckItemKindID = 3,Title = "Зачеркнутый"};

            Kinds= new List<ListCheckItemKind>
            {
                None,
                Simple,
                LineThrough
            };
        }

        /// <summary>
        /// Признак существования типа выделения.
        /// </summary>
        /// <param name="kindId">Код типа выделения.</param>
        /// <returns>Признак существования.</returns>
        public static bool Exists(int kindId)
        {
            return Kinds.Any(i => i.ListCheckItemKindID == kindId);
        }

        private static IList<ListCheckItemKind> Kinds { get; set; }

        /// <summary>
        /// Без выделения.
        /// </summary>
        public static ListCheckItemKind None { get; private set; }

        /// <summary>
        /// Простой
        /// </summary>
        public static ListCheckItemKind Simple { get; private set; }

        /// <summary>
        /// Зачеркнутый.
        /// </summary>
        public static ListCheckItemKind LineThrough { get; private set; }
    }
}
