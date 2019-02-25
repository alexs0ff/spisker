using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Core.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Core.Common.Repositories
{
    /// <summary>
    /// Репозиторий для сущности.
    /// </summary>
    public interface IEntityRepository<TEntity, TKey> : IRepository
        where TKey : struct
        where TEntity : EntityBase<TKey>
    {
        DbSet<TEntity> GetEntities();

        Task<TEntity> GetById(TKey id);

        /// <summary>
        /// Удаляет из контекста определенную сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        void Detach(TEntity entity);
    }
}
