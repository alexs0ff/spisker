using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Entities;

namespace Server.Core.Common.Repositories
{
    /// <summary>
    /// Базовый репозитрий для одной сущности.
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    /// <typeparam name="TKey">Tkey</typeparam>
    /// <typeparam name="TDbContext">TDbContext</typeparam>
    /// <typeparam name="TDbContextFactory">Тип фабрики контекста.</typeparam>
    public abstract class EntityRepositoryBase<TEntity, TKey, TDbContext, TDbContextFactory> : RepositoryBase<TDbContext, TDbContextFactory>, IEntityRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : struct
        where TDbContext : DbContext
        where TDbContextFactory : DbContextFactory<TDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        protected EntityRepositoryBase(TDbContextFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Получает сущность для управления.
        /// </summary>
        public DbSet<TEntity> Entities { get { return GetContext().Set<TEntity>(); } }

        /// <summary>
        /// Получает доступ к сущности.
        /// </summary>
        /// <returns>IQueryable типа сущности.</returns>
        public DbSet<TEntity> GetEntities()
        {
            var context = GetContext();
            return context.Set<TEntity>();
        }

        /// <summary>
        /// Получает сущность по коду.
        /// </summary>
        /// <param name="id">Код сущности.</param>
        /// <returns>Сущность.</returns>
        public Task<TEntity> GetById(TKey id)
        {
            var context = GetContext();
            return context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Получает доступ к сущности.
        /// </summary>
        /// <returns>IQueryable типа сущности.</returns>
        public void Detach(TEntity entity)
        {
            var context = GetContext();
            var attached = context.Entry(entity);

            if (attached!=null)
            {
                attached.State = EntityState.Detached;
            }
        }
    }
}
