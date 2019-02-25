using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Core.Common.Entities;

namespace Server.Core.Common.Repositories
{
    /// <summary>
    /// Репозиторий для основных операций с сущностями.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <typeparam name="TKey">Тип ключа сущности.</typeparam>
    public interface ICrudRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : EntityBase<TKey>
    {
        /// <summary>
        /// Сохраняет новую сущность в базе.
        /// </summary>
        /// <param name="entity">Сохраняемая сущность.</param>
        /// <returns>Значение созданного ключа.</returns>
        Task<TKey> SaveNew(TEntity entity);

        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        /// <param name="entity">Сущность для обновления.</param>
        /// <returns>Task.</returns>
        Task Update(TEntity entity);

        /// <summary>
        /// Удаление сущности из репозитория.
        /// </summary>
        /// <param name="id">Код сущности.</param>
        Task Delete(TKey id);
    }
}
