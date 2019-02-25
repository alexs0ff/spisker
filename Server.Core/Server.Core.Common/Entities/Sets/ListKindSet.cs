using System.Collections.Generic;
using System.Linq;
using Server.Core.Common.Entities.Lists;

namespace Server.Core.Common.Entities.Sets
{
    /// <summary>
    /// Справочник по типам отображения пунктов списка.
    /// </summary>
    public static class ListKindSet
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        static ListKindSet()
        {
            Simple = new ListKind { ListKindID = 1, Title = "Простой" };
            Marker = new ListKind { ListKindID = 2, Title = "Маркер" };
            Numerable = new ListKind { ListKindID = 3, Title = "Нумерованный" };
            Kinds = new List<ListKind>
            {
                Simple,
                Marker,
                Numerable
            };
        }

        /// <summary>
        /// Признак существования типа списка.
        /// </summary>
        /// <param name="kindId">Код типа списка.</param>
        /// <returns>Признак существования.</returns>
        public static bool Exists(int kindId)
        {
            return Kinds.Any(i => i.ListKindID == kindId);
        }

        private static IList<ListKind> Kinds { get; set; }

        /// <summary>
        /// Простой список.
        /// </summary>
        public static ListKind Simple { get; private set; }

        /// <summary>
        /// Маркированный список.
        /// </summary>
        public static ListKind Marker { get; private set; }

        /// <summary>
        /// Нумерованный список.
        /// </summary>
        public static ListKind Numerable { get; private set; }
    }
}
