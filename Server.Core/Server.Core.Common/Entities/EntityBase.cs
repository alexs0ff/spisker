using System;
using System.Collections.Generic;
using System.Text;
using Server.Core.Common.Reflection;

namespace Server.Core.Common.Entities
{
    /// <summary>
    /// Базовый класс для всех моделей сущностей.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа.</typeparam>
    public abstract class EntityBase<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public abstract TKey GetId();

        /// <summary>
        /// Копирует сущность в другую сущность.
        /// </summary>
        /// <param name="entity">Код сущности.</param>
        public virtual void CopyTo(EntityBase<TKey> entity)
        {
            PropetiesCopier.CopyPropertiesTo(this, entity);
        }
    }
}
